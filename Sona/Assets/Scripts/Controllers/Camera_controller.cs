using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_controller : MonoBehaviour {

	private const float rot_speed = 2.5f;
	private const float narrow = 2.5f;
	private const float speed = 2f;

	private float base_dist;
	private float dist;
	private Transform tr;
	private Transform p_tr;
	private bool toFirst = false;
	private bool toThird = false;
	//positioned only for testing purposes
	public Vector3 mov;
	public int layerMask;
	
	public GameObject player;

	// Use this for initialization
	void Start () {
		Vector3 offset;
		
		offset = player.transform.position - tr.position;
		base_dist = offset.magnitude;
		dist = base_dist;
		layerMask = ~ (1 << 9);
	}

	void Awake () {
		tr = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {

	}


	void LateUpdate () {
		//Vector3 mov;
		Quaternion new_rotation;
		RaycastHit hit;
		
		float rho; //angle between the x axis and the projection of the vector on the horizontal
		float phi; //angle between the vertical and the vector;
		float h;
		float x = 0;
		float y = 0;
		float z = -1;

		if (Input.GetKey("i")){
			new_rotation = Quaternion.AngleAxis (-rot_speed, Vector3.right);
			tr.rotation = tr.rotation * new_rotation;
			if (tr.rotation.eulerAngles.x < 315 && tr.rotation.eulerAngles.x > 270){
				tr.rotation = Quaternion.Euler(
					315,
					tr.rotation.eulerAngles.y,
					0
				);
			}
			
			
		} else if (Input.GetKey("k")){
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

		if (Input.GetKey("l")){
			new_rotation = Quaternion.AngleAxis (rot_speed, Vector3.up);
			tr.rotation = new_rotation * tr.rotation;
		} else if (Input.GetKey("j")){
			new_rotation = Quaternion.AngleAxis (-rot_speed, Vector3.up);
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
	
			if (Physics.Raycast (player.transform.position, mov, out hit, dist, layerMask)){
				mov = mov * (hit.distance/dist);
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