using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using ApplicationConstants;

public class CameraController : MonoBehaviour {

    private const float rot_speed = 2.5f;
    //private const float narrow = 2.5f;
    private const float speed = 2f;

    private float base_dist;
    private float dist;
    private Transform tr;
    private Transform p_tr;
    private bool toFirst = false;
    private bool toThird = false;
    private int layerMask;

    //inputs
    private float v_rotation;
    private float h_rotation;

    private float c_dist = 0f;
    private bool active = true;
	
    private Player player;
    private CapsuleCollider collider;

    // Use this for initialization
    void Start () {
        //Vector3 offset;
        Camera cam = gameObject.GetComponent<Camera>();

        cam.nearClipPlane = 0.01f;
        c_dist = 0.3f;

        player = GetComponentInParent<Player>();
        collider = GetComponentInParent<CapsuleCollider>();
        //offset = player.transform.position - tr.position;
        //base_dist = offset.magnitude;
        base_dist = 2;
        tr.rotation = Quaternion.Euler(15, 0, 0);
        dist = base_dist;
        layerMask = 1 << 11 | 1 << 8 | 1 << 10 | 1 << 14;

    }

    void Awake () {
        tr = GetComponent<Transform> ();
    }
	
    // Update is called once per frame
    /*void Update () {

    }*/


    void LateUpdate () {
        Vector3 mov;
        Vector3 target;
        Quaternion new_rotation;
        RaycastHit hit;
            
        float rho; //angle between the x axis and the projection of the vector on the horizontal
        float phi; //angle between the vertical and the vector;
        float h;
        float x = 0;
        float y = 0;
        float z = -1;

        CheckInputs();

        target = player.transform.position + collider.center;
        if (Mathf.Abs(v_rotation) > Mathf.Epsilon){
            new_rotation = Quaternion.AngleAxis (v_rotation, Vector3.right);
            tr.rotation = tr.rotation * new_rotation;
            if (tr.rotation.eulerAngles.x < 315 && tr.rotation.eulerAngles.x > 270){
                tr.rotation = Quaternion.Euler(315, tr.rotation.eulerAngles.y, 0);
            } else if (tr.rotation.eulerAngles.x > 45 && tr.rotation.eulerAngles.x < 90){
                tr.rotation = Quaternion.Euler(45, tr.rotation.eulerAngles.y, 0);
            }
        }
        if (Mathf.Abs (h_rotation) > Mathf.Epsilon){
            new_rotation = Quaternion.AngleAxis (h_rotation, Vector3.up);
            tr.rotation = new_rotation * tr.rotation;
        }
                

        if (toFirst){
            ToFirstPerson();
        }
		
        if (toThird){
            ToThirdPerson();
        }

        if (dist > 0){
            //compute camera position
            rho = tr.rotation.eulerAngles.y;
            phi = tr.rotation.eulerAngles.x;
	
            //compute offset with respect to x and z
            h = dist * Mathf.Cos (phi * Mathf.Deg2Rad);
            z = -h * Mathf.Cos (rho * Mathf.Deg2Rad);

            x = -h * Mathf.Sin (rho * Mathf.Deg2Rad);
            //compute offset with respect to y
            y = base_dist * Mathf.Sin(phi * Mathf.Deg2Rad);
	
            mov = new Vector3 (x,y,z);

            Debug.DrawRay(target, mov * dist, Color.green);
            bool doesHit=Physics.Raycast(target, mov, out hit, dist + c_dist, layerMask);
            //print(doesHit);
            if (doesHit){
                mov = mov * ((hit.distance - c_dist)/dist);
            }

            tr.position = target + mov; 
        } else {
        tr.position = target;
        }
        resetInputs();
    }

    private void CheckInputs () {
        if (active){
            if (CrossPlatformInputManager.GetButton(CameraConstants.CameraUp)){
                v_rotation = v_rotation + rot_speed;
            }
            if (CrossPlatformInputManager.GetButton(CameraConstants.CameraDown)){
                v_rotation = v_rotation - rot_speed;
            }
            if (CrossPlatformInputManager.GetButton(CameraConstants.CameraLeft)){
                h_rotation = h_rotation - rot_speed;
            }
            if (CrossPlatformInputManager.GetButton(CameraConstants.CameraRight)){
                h_rotation = h_rotation + rot_speed;
            }
        }
    }

    /*public void SetPlayer (GameObject player){
        this.player = player;
    }*/

    public void SetInputs (float v_rotation, float h_rotation){
        if (!active){
            this.h_rotation = h_rotation;
            this.v_rotation = v_rotation;
        }
    }

    public void OnEnable () {
        this.active = true;
        resetInputs();
    }

    public void OnDisable () {
        this.active = false;
        resetInputs();
    }

    private void resetInputs (){
        h_rotation = 0;
        v_rotation = 0;
    }

    //reduces the distance from the player to move the camera towards a first person one
    private void ToFirstPerson (){
        dist = dist - speed * Time.deltaTime;
        if (dist < 0){
            dist = 0;
            toFirst = false;
        }
    }

    //increases the distance of the camera from the player until the third person camera position
    private void ToThirdPerson () {
        dist = dist + speed * Time.deltaTime;
        if (dist > base_dist){
            dist = base_dist;
            toThird = false;
        }
    }

	
    //set the boolean to start the transition to the first person camera if no transition is already happening
    public void MoveToFirst (){
        if (!(toFirst || toThird)){
            toFirst = true;
        }
    }

    //set the boolean to start the transition to the third person camera if no transition is already happening
    public void MoveToThird (){
        if (!(toFirst || toThird)){
            toThird = true;
        }
    }
}