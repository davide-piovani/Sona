using UnityEngine;
using ApplicationConstants;
using UnityStandardAssets.CrossPlatformInput;
using System;

public abstract class Player : MonoBehaviour {

    [SerializeField] Camera characterCamera;

    private float speed = GameConstants.runningSpeed;

    protected float powerDuration;
    protected float rechargeSpeed;
    protected float powerTimeLeft;

    public bool active = false;
    public bool protectToChangeCharacter = false;
    protected bool powerActive = false;
    //private ActiveCharacterController controller;
    private GameController gameController;


    //Animations
    public bool isGrounded;
    private float w_speed = 2f;
    private float rotSpeed = 85f;
    public float jumpHeight = 200f;
    Rigidbody rb;
    Animator anim;
    CapsuleCollider col_size;

    public bool isPowerActive() { return powerActive; }
    public Camera GetCharacterCamera() { return characterCamera; }

    protected void Start(){
        //controller = FindObjectOfType<ActiveCharacterController>();
        gameController = FindObjectOfType<GameController>();

        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        col_size = GetComponent<CapsuleCollider>();
        isGrounded = true;

        LoadPowerSettings();
        powerTimeLeft = powerDuration;
        gameController.UpdatePowerLevelIndicator(powerTimeLeft / powerDuration);
        PowerToggle(false);

        anim.speed = GameConstants.animationsSpeed;
    }

    protected abstract void LoadPowerSettings();

    private void Update () {
        if (active) {
            checkChangePlayer();
            checkPower();
            performAnimations();
        }
        checkPowerDuration();
    }

    private void checkPower(){
        if (CrossPlatformInputManager.GetButtonDown(GameConstants.powerButtonName)) {
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

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == GameConstants.killingObjectTag) {
            print("Death");
        }
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
            speed = (z < 0) ? w_speed : GameConstants.runningSpeed;
            if (Math.Abs(z) > Mathf.Epsilon){
                setAnimBools(Mode.running);
                //anim.SetBool("backward", z < 0);
            } else {
                setAnimBools(Mode.idle);
            }
        }

        //rotSpeed = 2f;
        transform.Translate(0, 0, z*speed*Time.deltaTime);
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
