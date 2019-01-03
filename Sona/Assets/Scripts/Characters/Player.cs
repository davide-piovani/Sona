using UnityEngine;
using ApplicationConstants;
using UnityStandardAssets.CrossPlatformInput;
using System;
using UnityEngine.AI;

public abstract class Player : InputListener {

    [SerializeField] Camera characterCamera;

    private float speed = PlayersConstants.runningSpeed;

    protected float powerDuration;
    protected float rechargeSpeed;
    protected float powerTimeLeft;

    protected int layerMask;
    float radius;

    protected PlayerType type;
    public bool active = true;
    public bool powerActive = false;
    private GameController gameController;

    //Player inputs
    Vector3 direction;
    private bool walks;

    //Animations
    //public bool isGrounded;
    protected float r_speed = PlayersConstants.runningSpeed;
    private float w_speed = 2f;

    Rigidbody rb;
    Animator anim;
    CapsuleCollider col_size;
    NavMeshAgent agent;
    private GameObject avatar;

    public bool IsPowerActive() { return powerActive; }
    public Camera GetCharacterCamera() { return characterCamera; }

    protected void Start(){
        LoadComponents();

        LoadPowerInfo();
        SetType();

        ManageLayers();

        //anim.speed = GameConstants.animationsSpeed;
        anim.speed = 1f;


	    //if (FindObjectOfType<CheckpointController>()){ FindObjectOfType<CheckpointController>().RestorePlayerCheckpoint(this);}
        resetInputs();
    }

    private void LoadComponents() {
        gameController = FindObjectOfType<GameController>();

        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        col_size = GetComponent<CapsuleCollider>();
        agent = GetComponent<NavMeshAgent>();

        avatar = GetComponent<Transform>().GetChild(0).gameObject;
    }

    private void LoadPowerInfo() {
        LoadPowerSettings();
        powerTimeLeft = powerDuration;
        gameController.UpdatePowerLevelIndicator(powerTimeLeft / powerDuration);
        PowerToggle(false);
    }

    private void SetType(){
        Hannah hannah = GetComponent<Hannah>();
        if (hannah) type = PlayerType.Hannah;

        Jack jack = GetComponent<Jack>();
        if (jack) type = PlayerType.Jack;

        Charlie charlie = GetComponent<Charlie>();
        if (charlie) type = PlayerType.Charlie;
    }

    private void ManageLayers(){
        if (GetComponent<Transform>().localScale[0] > GetComponent<Transform>().localScale[2]) {
            radius = col_size.radius * (GetComponent<Transform>()).localScale[0];
        } else {
            radius = col_size.radius * (GetComponent<Transform>()).localScale[2];
        }
        //layerMask = ~(1 << 2 | 1 << 9);
        layerMask = 1 << 8 | 1 << 10 | 1 << 11 | 1 << 13;
    }

    protected abstract void LoadPowerSettings();


    private void Update () {

        if (IsInputActive()){
            checkInputs();
            performAnimations();
            checkPower();
        }


        checkPowerDuration();
    }

    private void checkPower(){
        if (CrossPlatformInputManager.GetButtonDown(PlayersConstants.powerButtonName)) {
            PowerToggle(!powerActive);
        }
    }

    protected abstract void PowerToggle(bool isActive);

    private void checkPowerDuration(){
        
        if (powerActive) {
            powerTimeLeft -= Time.deltaTime;

            if (powerTimeLeft <= 0) PowerToggle(false);
        } else {
            if (powerTimeLeft < powerDuration) {
                powerTimeLeft += Time.deltaTime * rechargeSpeed;
            } else {
                powerTimeLeft = powerDuration;
            }
        }
        
        //gameController.UpdatePowerLevelIndicator(this);  //powerTimeLeft , powerDuration, powerActive);
        gameController.UpdatePowerLevelIndicator(powerTimeLeft / powerDuration);
        //print("Power time left: " + powerTimeLeft.ToString());
    }

    private void checkChangePlayer(){
        /*if (CrossPlatformInputManager.GetButtonDown(GameConstants.changeCharacterButton)){
            if (!protectToChangeCharacter) {
                print("NextCharacter");
                controller.NextCharacter();
            }
            protectToChangeCharacter = false;
        } else {
            protectToChangeCharacter = false;
        }*/
    }

    enum Mode {
        idle,
        walking,
        running
    }

    private void performAnimations(){
        float angle;

        if (walks){
            //Walking
            speed = w_speed;
            if (direction.magnitude > Mathf.Epsilon) {
                setAnimBools(Mode.walking);
            } else {
                setAnimBools(Mode.idle);
            }
        } else {
            //Running
            speed = r_speed;
            //speed = (z < 0) ? w_speed : r_speed;
            if (direction.magnitude > Mathf.Epsilon){
                setAnimBools(Mode.running);
                //anim.SetBool("backward", z < 0);
            } else {
                setAnimBools(Mode.idle);
            }
        }

        Transform tr = gameObject.GetComponent<Transform>();
        RaycastHit data;
        direction.Normalize();
        Vector3 movement;
        if (type == PlayerType.Jack){
            movement = direction * speed * Time.deltaTime;
        } else {
            movement = direction * speed * TimeController.GetDelTaTime();
        }
        if (Physics.Raycast (tr.position + col_size.center, movement, 
        out data, radius + movement.magnitude, layerMask)){
    	    float x_collision = (data.point[0] - tr.position[0] - radius * Mathf.Abs(direction[0]));
            float z_collision = (data.point[2] - tr.position[2] - radius * Mathf.Abs(direction[2]));
    	    movement = new Vector3 (x_collision, 0, z_collision);
    	}
        tr.position = transform.position + movement;

        if (direction.magnitude > Mathf.Epsilon){
            //Rotate the image to get the correct animation
            angle = Vector3.SignedAngle (tr.forward, direction, Vector3.up);
            avatar.transform.rotation = Quaternion.Euler (-90, angle, 0);
        }
        if (!(tr.rotation == Quaternion.identity)){
            print ("Adjusting");
	    Transform camTr = characterCamera.GetComponent<Transform>();
            angle = tr.rotation.eulerAngles.y;
            tr.rotation = Quaternion.identity;
            camTr.rotation = Quaternion.Euler(camTr.rotation.eulerAngles.x, angle,0);
            avatar.transform.rotation = Quaternion.Euler(-90, 0, angle);
        }
        resetInputs();
    }

    private void setAnimBools(Mode mode){
        bool idle = false;
        bool walking = false;
        bool running = false;

        switch (mode){
            case Mode.idle:
                idle = true;
                break;
            case Mode.walking:
                walking = true;
                break;
            case Mode.running:
                running = true;
                break;
        }

        anim.SetBool("isIdle", idle);
        anim.SetBool("isWalking", walking);
        anim.SetBool("isRunning", running);
    }

    //checks the inputs of the user, if activated
    private void checkInputs (){
        float h_axis;
        float v_axis;
        Vector3 forward;
        Vector3 right;


        if (active){
            h_axis = CrossPlatformInputManager.GetAxis ("Horizontal");
            v_axis = CrossPlatformInputManager.GetAxis ("Vertical");
            forward = characterCamera.GetComponent<Transform>().forward;
            right = characterCamera.GetComponent<Transform>().right;

            forward.y = 0;
            forward.Normalize ();

            right.y = 0;
            right.Normalize ();

            this.direction = forward * v_axis + right * h_axis;

//------------------Add walk button to crossPlatformInputManager
        this.walks = Input.GetKey(KeyCode.LeftShift);
//--------------------------------------------------
        }
    }

    //reset inputs once the Update is done
    private void resetInputs (){
        this.direction = new Vector3 (0,0,0);
        this.walks = false;
    }


    //used to force input by script, the value will not be resetted until the following
    //Update. The character must not be taking input from the user
    public void setInputs (Vector3 direction, bool jump, bool walks, bool power){
        if (!active){
            this.direction = direction;
            this.walks = walks;
            PowerToggle(power);
        }
    }

    
    public void Activate (){
        this.active = true;
        characterCamera.enabled = true;
        characterCamera.gameObject.GetComponent<AudioListener>().enabled = true;
        resetInputs();
        ActiveInput();
    }

    public void Deactivate (){
        this.active = false;
        characterCamera.enabled = false;
        characterCamera.gameObject.GetComponent<AudioListener>().enabled = false;
        resetInputs();
        DisableInput();
        if (anim != null){
            setAnimBools (Mode.idle);
        }
    }

    public bool IsVisible() {
        if (type == PlayerType.Hannah) return !IsPowerActive();
        return true;
    }

    public PlayerType GetPlayerType() { return type; }

}
