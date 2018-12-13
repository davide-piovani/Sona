using UnityEngine;
using ApplicationConstants;
using UnityStandardAssets.CrossPlatformInput;
using System;
using UnityEngine.AI;

public abstract class Player : MonoBehaviour {

    [SerializeField] Camera characterCamera;

    private float speed = PlayersConstants.runningSpeed;

    protected float powerDuration;
    protected float rechargeSpeed;
    protected float powerTimeLeft;

    protected int layerMask;
    float radius;

    public bool active = false;
    public bool protectToChangeCharacter = false;
    protected bool powerActive = false;
    //private ActiveCharacterController controller;
    private GameController gameController;


    //Animations
    //public bool isGrounded;
    private float w_speed = 2f;
    private float rotSpeed = 95f;
    //public float jumpHeight = 200f;
    Rigidbody rb;
    Animator anim;
    CapsuleCollider col_size;
    NavMeshAgent agent;

    public bool isPowerActive() { return powerActive; }
    public Camera GetCharacterCamera() { return characterCamera; }

    protected void Start(){
        LoadComponents();

        //isGrounded = true;
        LoadPowerInfo();

        ManageLayers();

        //anim.speed = GameConstants.animationsSpeed;
        anim.speed = 1f;

	if (FindObjectOfType<CheckpointController>()){
        FindObjectOfType<CheckpointController>().RestorePlayerCheckpoint(this);}
    }

    private void LoadComponents() {
        //controller = FindObjectOfType<ActiveCharacterController>();
        gameController = FindObjectOfType<GameController>();

        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        col_size = GetComponent<CapsuleCollider>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void LoadPowerInfo() {
        LoadPowerSettings();
        powerTimeLeft = powerDuration;
        gameController.UpdatePowerLevelIndicator(powerTimeLeft / powerDuration);
        PowerToggle(false);
    }

    private void ManageLayers(){
        if (GetComponent<Transform>().localScale[0] > GetComponent<Transform>().localScale[2]) {
            radius = col_size.radius * (GetComponent<Transform>()).localScale[0];
        } else {
            radius = col_size.radius * (GetComponent<Transform>()).localScale[2];
        }
        //layerMask = ~(1 << 2 | 1 << 9);
        layerMask = 1 << 8 | 1 << 10 | 1 << 11;
    }

    protected abstract void LoadPowerSettings();

    private void Update () {
        if (active) {
            checkChangePlayer();
            checkPower();
            performAnimations();
        }
        checkPowerDuration();
        if (Input.GetKeyDown(KeyCode.T)) { FindObjectOfType<CheckpointController>().printCheck(); }
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
        var z = CrossPlatformInputManager.GetAxis("Vertical");
        var y = CrossPlatformInputManager.GetAxis("Horizontal");


        /*if (isGrounded){
            if (CrossPlatformInputManager.GetButtonDown(GameConstants.jumpButton)){
                rb.AddForce(0, jumpHeight, 0);
                anim.SetTrigger("isJumping");
                isGrounded = false;
            }
        } else {
            if (rb.velocity.y > 0) {
                anim.SetInteger("jumpMode", 0);
            } else if (rb.velocity.y < 0) {
                anim.SetInteger("jumpMode", 2);
            } else {
                anim.SetInteger("jumpMode", 2);
                isGrounded = true;
            }
        }*/


        if (Input.GetKey(KeyCode.LeftShift)){
            //Walking
            speed = w_speed;
            if (Math.Abs(z) > Mathf.Epsilon) {
                setAnimBools(Mode.walking);
            } else {
                setAnimBools(Mode.idle);
            }
        } else {
            //Running
            speed = (z < 0) ? w_speed : PlayersConstants.runningSpeed;
            if (Math.Abs(z) > Mathf.Epsilon){
                setAnimBools(Mode.running);
                //anim.SetBool("backward", z < 0);
            } else {
                setAnimBools(Mode.idle);
            }
        }

        Transform tr = gameObject.GetComponent<Transform>();
        RaycastHit data;
        Vector3 movement = new Vector3 (0, 0, z*speed*Time.deltaTime);
        if (Physics.Raycast (tr.position + col_size.center, Mathf.Sign(z) * tr.forward, 
        out data, radius + movement.magnitude, layerMask)){
    	    float x_collision = (data.point[0] - tr.position[0] - radius * Mathf.Sign(z) * tr.forward[0]);
            float z_collision = (data.point[2] - tr.position[2] - radius * Mathf.Sign(z) * tr.forward[2]);
    	    movement = new Vector3 (x_collision, 0, z_collision);
            tr.position = transform.position + movement;
    	} else {
            transform.Translate(movement);
    	}
        transform.Rotate(0, y*rotSpeed*Time.deltaTime, 0);
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
}
