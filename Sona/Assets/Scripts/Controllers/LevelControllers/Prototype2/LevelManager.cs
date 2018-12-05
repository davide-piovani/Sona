using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	private Vector3 checkPosition = new Vector3 (0,0,0);
	private Quaternion checkRotation = Quaternion.Euler (0,0,0);

	private Vector3 [] p_position = new Vector3 [15];
	private Platform [] p_script = new Platform [15];
	private bool open = false;

	public GameObject player;
	public GameObject prefab;
	public GameObject MovableWall1;
	public GameObject MovableWall2;
	public LastTrapMobileWall MobileWall3;

	// Use this for initialization
	void Start () {
		GameObject obj;
		int i;

		p_position [0] = new Vector3 (3f, -0.25f, 23f);
		p_position [1] = new Vector3 (6f, -0.25f, 23f);
		p_position [2] = new Vector3 (9f, -0.25f, 23f);
		p_position [3] = new Vector3 (9f, -0.25f, 20f);
		p_position [4] = new Vector3 (6f, -0.25f, 20f);
		p_position [5] = new Vector3 (3f, -0.25f, 20f);
		p_position [6] = new Vector3 (3f, -0.25f, 17f);
		p_position [7] = new Vector3 (6f, -0.25f, 17f);
		p_position [8] = new Vector3 (9f, -0.25f, 17f);
		p_position [9] = new Vector3 (12f, -0.25f, 17f);
		p_position [10] = new Vector3 (2f, -0.25f, 10f);
		p_position [11] = new Vector3 (2f, -0.25f, 3.5f);
		p_position [12] = new Vector3 (7f, -0.25f, 3.5f);
		p_position [13] = new Vector3 (7f, -0.25f, 10f);
		p_position [14] = new Vector3 (11f, -0.25f, 10f);

		for (i=0; i<15; i++){
			p_script[1] = null;
		}

		//Instantiate the first platform
		obj = Instantiate (prefab, p_position[0], Quaternion.Euler (-90,0,0));
		p_script [0] = obj.AddComponent<Platform>() as Platform;
		p_script [0].SetData (0, this);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayerDies(){
		player.transform.position = checkPosition;
		player.transform.rotation = checkRotation;
	}

	public void CheckpointReached (Vector3 position, Quaternion rotation){
		checkPosition = position;
		checkRotation = rotation;
	}

	public void PlatformTriggered (int id){
		GameObject obj;
		if (id <  14 && p_script[id+1] == null){
			obj = Instantiate (prefab, p_position[id + 1], Quaternion.Euler (-90,0,0));
			p_script [id+1] = obj.AddComponent<Platform>() as Platform;
			p_script [id+1].SetData (id+1, this);
		}
		if (id == 9){
			Destroy (MovableWall1);
		}
	}

	public void ButtonMoveWall (bool open){
		Vector3 mov = new Vector3 (0, 5, 0);
		if (open && !this.open){
			MovableWall2.transform.position = MovableWall2.transform.position + mov;
			this.open = true;
		} else if (!open && this.open){
			MovableWall2.transform.position = MovableWall2.transform.position - mov;
			this.open = false;
		}
	}

	public void LastTrapTrigger (){
		MobileWall3.Open();
	}

	
}
