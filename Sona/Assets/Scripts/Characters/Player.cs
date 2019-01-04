using UnityEngine;
using UnityEngine.UI;
using ApplicationConstants;
using UnityStandardAssets.CrossPlatformInput;
using System;
using UnityEngine.AI;

public abstract class Player : InputListener {

    private Camera characterCamera;
    [SerializeField] Sprite characterPortrait;

    private float speed = PlayersConstants.runningSpeed;

    protected float powerDuration;
    protected float rechargeSpeed;
    protected float powerTimeLeft;

    protected int layerMask;
    private float playerColliderRadius;

    protected PlayerType type;
    //public bool active = true;
    public bool powerActive = false;
    private GameController gameController;

    //Player inputs
    Vector3 direction;
    //private bool walks;

    //Animations
    //public bool isGrounded;
    //protected float r_speed = PlayersConstants.runningSpeed;
    //private float w_speed = 2f;

    //private Rigidbody rb;
    private Animator anim;
    private CapsuleCollider playerCollider;
    private NavMeshAgent agent;
    private AudioSource audioSource;
    [SerializeField] GameObject avatar;

    public bool IsPowerActive() { return powerActive; }
    public Camera GetCharacterCamera() { return characterCamera; }

    private void Awake (){
        LoadComponents();

        LoadPowerInfo();
        SetType();
    }

    protected void Start(){
        ManageLayers();

        //anim.speed = GameConstants.animationsSpeed;
        anim.speed = 1f;


	    //if (FindObjectOfType<CheckpointController>()){ FindObjectOfType<CheckpointController>().RestorePlayerCheckpoint(this);}
        //ResetInputs();
    }


    //Get a reference to all player components
    private void LoadComponents() {
        gameController = FindObjectOfType<GameController>();

        characterCamera = GetComponentInChildren<Camera>();
        //rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider>();
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();

        avatar = transform.GetChild(0).gameObject;
    }

    //Load the duration of the power for this specific player and its recharge time
    private void LoadPowerInfo() {
        LoadPowerSettings();
        powerTimeLeft = powerDuration;
        PowerToggle(false);
    }

    protected abstract void LoadPowerSettings();

    //Set the type of the player based on the script associated
    private void SetType(){
        if (GetComponent<Hannah>()) type = PlayerType.Hannah;

        if (GetComponent<Jack>()) type = PlayerType.Jack;

        if (GetComponent<Charlie>()) type = PlayerType.Charlie;
    }

    private void ManageLayers(){
        float radius;

        if (GetComponent<Transform>().localScale[0] > GetComponent<Transform>().localScale[2]) {
            radius = GetComponent<CapsuleCollider>().radius * (GetComponent<Transform>()).localScale[0];
        } else {
            radius = GetComponent<CapsuleCollider>().radius * (GetComponent<Transform>()).localScale[2];
        }

        layerMask = 1 << 8 | 1 << 10 | 1 << 11 | 1 << 13;
        float localScale = (transform.localScale[0] > transform.localScale[2]) ? 
            (transform.localScale[0]) : (transform.localScale[2]);

        playerColliderRadius = playerCollider.radius * localScale;
    }

    private void Update () {
        if (IsInputActive()){
            //CheckInputs();
            GetPlayerDirection();
            MoveCharacter();
            ManagePower();
        }

        CheckPowerDuration();
    }

    private void ManagePower(){
        if (CrossPlatformInputManager.GetButtonDown(PlayersConstants.powerButtonName)) {
            PowerToggle(!powerActive);
        }
    }

    protected abstract void PowerToggle(bool isActive);

    public Sprite GetCharacterPortrait(){ return characterPortrait; }

    private void CheckPowerDuration(){
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

        if (IsInputActive()) gameController.UpdatePowerLevelIndicator(powerTimeLeft / powerDuration);
    }

    enum Mode {
        idle,
        walking,
        running
    }

    private void MoveCharacter(){
        GetPlayerDirection();
        ManagePlayerSpeedRotationAndAnimation();

        direction.Normalize();
        Vector3 movement = direction * speed * Time.deltaTime;

        if (!WillHitSomething(movement)) {
            transform.position += movement;

            PlayFootStep(movement.magnitude > Mathf.Epsilon);
        }


        //ResetInputs();
    }

    private bool WillHitSomething(Vector3 movement){
        return Physics.Raycast(transform.position + playerCollider.center, movement,
             playerColliderRadius + movement.magnitude, layerMask);
    }

    private void PlayFootStep(bool moving) {
        if (moving){
            if (!audioSource.isPlaying){
                audioSource.clip = AudioEffects.instance.footstep;
                audioSource.volume = gameController.GetEffectsVolume();
                audioSource.loop = true;
                audioSource.Play();
            }
        } else {
            audioSource.loop = false;
            audioSource.Stop();
        }
    }


    private void ManagePlayerSpeedRotationAndAnimation(){
        if (direction.magnitude > Mathf.Epsilon){
            if (direction.magnitude > PlayersConstants.runningMinimumMagnitude) {
                speed = PlayersConstants.runningSpeed;
                SetAnimBools(Mode.running);
            } else {
                speed = PlayersConstants.walkingSpeed;
                SetAnimBools(Mode.walking);
            }
            RotatePlayer();
        } else {
            speed = 0f;
            SetAnimBools(Mode.idle);
        }
    }

    //Rotate the image to get the correct animation
    private void RotatePlayer(){
        float angle = Vector3.SignedAngle(transform.forward, direction, Vector3.up);
        avatar.transform.rotation = Quaternion.Euler(-90, angle, 0);

        if (!(transform.rotation == Quaternion.identity)){
            Transform camTr = characterCamera.transform;
            angle = transform.rotation.eulerAngles.y;
            transform.rotation = Quaternion.identity;
            camTr.rotation = Quaternion.Euler(camTr.rotation.eulerAngles.x, angle, 0);
            avatar.transform.rotation = Quaternion.Euler(-90, 0, angle);
        }
    }

    private void SetAnimBools(Mode mode){
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

    

    private void GetPlayerDirection(){
        float h_axis = CrossPlatformInputManager.GetAxis("Horizontal");
        float v_axis = CrossPlatformInputManager.GetAxis("Vertical");

        Vector3 forward = characterCamera.transform.forward;
        Vector3 right = characterCamera.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        direction = forward * v_axis + right * h_axis;
    }

    
    public void Activate (){
        //this.active = true;
        characterCamera.enabled = true;
        characterCamera.gameObject.GetComponent<AudioListener>().enabled = true;
        //ResetInputs();
        ActiveInput();
    }

    public void Deactivate (){
        //this.active = false;
        characterCamera.enabled = false;
        characterCamera.gameObject.GetComponent<AudioListener>().enabled = false;
        //SetAnimBools(Mode.idle);
        //ResetInputs();
        DisableInput();
        if (anim != null){
            SetAnimBools (Mode.idle);
        }
    }

    public bool IsVisible() {
        if (type == PlayerType.Hannah) return !IsPowerActive();
        return true;
    }

    public PlayerType GetPlayerType() { return type; }

    public void SetDestination(Vector3 destination) { agent.SetDestination(destination); }
}


//////////////////////////////////////////
// HERE ARE ALL THE DISCARDED FUNCTIONS //
/////////////////////////////////////////
/*
    private void PerformAnimations()
    {
        float angle;

        GetPlayerDirection();
        SetSpeedAndAnimation();

        Transform tr = gameObject.transform;
        RaycastHit data;
        direction.Normalize();

        Vector3 movement;
        if (type == PlayerType.Jack){
            movement = direction * speed * Time.deltaTime;
        } else {
            movement = direction * speed * TimeController.GetDelTaTime();
        }
        if (Physics.Raycast (tr.position + GetComponent<CapsuleCollider>().center, movement, 
        out data, radius + movement.magnitude, layerMask)){
    	    float x_collision = (data.point[0] - tr.position[0] - radius * Mathf.Abs(direction[0]));
            float z_collision = (data.point[2] - tr.position[2] - radius * Mathf.Abs(direction[2]));
    	    movement = new Vector3 (x_collision, 0, z_collision);
    	}

        tr.position = transform.position + movement;

        if (direction.magnitude > Mathf.Epsilon)
        {
            //Rotate the image to get the correct animation
            angle = Vector3.SignedAngle(tr.forward, direction, Vector3.up);
            avatar.transform.rotation = Quaternion.Euler(-90, angle, 0);
        }

        if (!(tr.rotation == Quaternion.identity)){
            print ("Adjusting");
	    Transform camTr = characterCamera.GetComponent<Transform>();
            angle = tr.rotation.eulerAngles.y;
            tr.rotation = Quaternion.identity;
            camTr.rotation = Quaternion.Euler(camTr.rotation.eulerAngles.x, angle, 0);
            avatar.transform.rotation = Quaternion.Euler(-90, 0, angle);
        }
        ResetInputs();
    }


    //used to force input by script, the value will not be resetted until the following
    //Update. The character must not be taking input from the user
    public void SetInputs (Vector3 direction, bool jump, bool walks, bool power){
        if (!active){
            this.direction = direction;
            this.walks = walks;
            PowerToggle(power);
        }
    }

    //reset inputs once the Update is done
    private void ResetInputs (){
        this.direction = new Vector3 (0,0,0);
        this.walks = false;
    }

    //checks the inputs of the user, if activated
    private void CheckInputs (){
        float h_axis;
        float v_axis;
        Vector3 forward;
        Vector3 right;


        if (active){
            h_axis = CrossPlatformInputManager.GetAxis ("Horizontal");
            v_axis = CrossPlatformInputManager.GetAxis ("Vertical");
            forward = characterCamera.transform.forward;
            right = characterCamera.transform.right;

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
*/
