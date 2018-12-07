using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float Speed = 5.5f;
    public float JumpHeight = 2f;
    public float GroundDistance = 0.2f;
    public float DashDistance = 5f;
    public float turnSmoothTime = 0.05f;
    public float speedSmoothTime = 0.1f;
    [Range(0, 1)]
    public float airControlPercent;
    public LayerMask Ground;
    public GameObject _environment;

    float turnSmoothVelocity;
    float speedSmoothVelocity;
    float currentSpeed;
    Transform _camera;
    Rigidbody _body;
    TimeController _timeController; //campo per ora non usato al posto di Time.deltaTime
    Animator _animator;
    Vector3 _inputs = Vector3.zero;
    Vector3 _inputsDir = Vector3.zero;
    bool _isGrounded = true;
    Transform _groundChecker;

    void Start()
    {
        _camera = Camera.main.transform;
        _body = GetComponent<Rigidbody>();
        _groundChecker = transform.GetChild(0);
        _animator = GetComponentInChildren<Animator>();
        _timeController = _environment.GetComponent<TimeController>();
    }

    void Update()
    {
        _isGrounded = Physics.CheckSphere(_groundChecker.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);


        //input
        _inputs = Vector3.zero;
        _inputs.x = Input.GetAxisRaw("Horizontal");
        _inputs.z = Input.GetAxisRaw("Vertical");
        _inputsDir = _inputs.normalized;

        if (_inputsDir != Vector3.zero)
        {
            float targetRotation = Mathf.Atan2(_inputsDir.x, _inputsDir.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, GetModifiedSmoothTime(turnSmoothTime));
            _animator.SetBool("run", true);
        }
        else
        {
            _animator.SetBool("run", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _body.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
            _animator.SetBool("jump", true);
        }
        else
        {
            _animator.SetBool("jump", false);
        }

        float targetSpeed = Speed * _inputsDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, GetModifiedSmoothTime(speedSmoothTime));

        /*if I have Dash
        if (Input.GetKeyDown(KeyCode.H))
        {
            Vector3 dashVelocity = Vector3.Scale(transform.forward, DashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * _body.drag + 1)) / -Time.deltaTime), 0, (Mathf.Log(1f / (Time.deltaTime * _body.drag + 1)) / -Time.deltaTime)));
            _body.AddForce(dashVelocity, ForceMode.VelocityChange);
        }
        */

    }

    void Move(Vector2 inputDir, bool running){

    }

    float GetModifiedSmoothTime(float smoothTime)
    {

        if (_isGrounded)
        {
            return smoothTime;
        }

        if (airControlPercent == 0)
        {
            return float.MaxValue;
        }

        return smoothTime / airControlPercent;
    }


    void FixedUpdate()
    {
        Vector3 velocity = transform.forward * currentSpeed;
        _body.MovePosition(_body.position + velocity * Time.deltaTime);
    }
}
