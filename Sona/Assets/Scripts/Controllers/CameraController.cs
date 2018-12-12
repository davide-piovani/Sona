using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using ApplicationConstants;

public class CameraController : MonoBehaviour {

	private const float rot_speed = 2.5f;
	private const float narrow = 2.5f;
	private const float speed = 2f;

	private float base_dist;
	private float dist;
	private Transform tr;
	private Transform p_tr;
	private bool toFirst = false;
	private bool toThird = false;
	private int layerMask;

	private float c_dist = 0f;
	
	public GameObject player;

	// Use this for initialization
	void Start () {
		Vector3 offset;
		Camera cam = gameObject.GetComponent<Camera>();

		cam.nearClipPlane = 0.01f;
		c_dist = 0.3f;
		
		offset = player.transform.position - tr.position;
		base_dist = offset.magnitude;
		dist = base_dist;
        layerMask = 1 << 11 | 1 << 8 | 1 << 10;

		//c_dist = compute_dist ();
        //print(c_dist);
	}

	void Awake () {
		tr = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {

	}


	void LateUpdate () {
		Vector3 mov;
		Quaternion new_rotation;
		RaycastHit hit;
		
		float rho; //angle between the x axis and the projection of the vector on the horizontal
		float phi; //angle between the vertical and the vector;
		float h;
		float x = 0;
		float y = 0;
		float z = -1;

		if (CrossPlatformInputManager.GetButton(CameraConstants.CameraUp)){
			new_rotation = Quaternion.AngleAxis (-rot_speed, Vector3.right);
			tr.rotation = tr.rotation * new_rotation;
			if (tr.rotation.eulerAngles.x < 315 && tr.rotation.eulerAngles.x > 270){
				tr.rotation = Quaternion.Euler(
					315,
					tr.rotation.eulerAngles.y,
					0
				);
			}
			
			
		} else if (CrossPlatformInputManager.GetButton(CameraConstants.CameraDown)){
			new_rotation = Quaternion.AngleAxis (rot_speed, Vector3.right);
			tr.rotation = tr.rotation * new_rotation;
			if (tr.rotation.eulerAngles.x > 45 && tr.rotation.eulerAngles.x < 90){
				tr.rotation = Quaternion.Euler(
					45,
					tr.rotation.eulerAngles.y,
					0
				);
			}
		}

		if (CrossPlatformInputManager.GetButton(CameraConstants.CameraRight)){
			new_rotation = Quaternion.AngleAxis (rot_speed, Vector3.up);
			tr.rotation = new_rotation * tr.rotation;
		} else if (CrossPlatformInputManager.GetButton(CameraConstants.CameraLeft)){
			new_rotation = Quaternion.AngleAxis (-rot_speed, Vector3.up);
			tr.rotation = new_rotation * tr.rotation;
		} else if (System.Math.Abs(CrossPlatformInputManager.GetAxis("Vertical")) > 0.0001f)
        {
            //puts the camera behind the player when moving
            new_rotation = Quaternion.Euler(30, player.transform.rotation.eulerAngles[1], 0);
			tr.rotation = Quaternion.RotateTowards (tr.rotation, new_rotation, rot_speed);
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

            Debug.DrawRay(player.transform.position, mov * dist, Color.green);
            bool doesHit=Physics.Raycast(player.transform.position, mov, out hit, dist + c_dist, layerMask);
            //print(doesHit);
            if (doesHit){
				mov = mov * ((hit.distance - c_dist)/dist);
			}

			tr.position = player.transform.position + mov; 
		}
		else{
			tr.position = player.transform.position;
		}
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