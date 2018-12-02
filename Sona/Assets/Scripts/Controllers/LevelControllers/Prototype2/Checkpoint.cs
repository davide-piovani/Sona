using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

	public LevelManager manager;
	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnCollisionEnter (Collision other){
		if (other.collider.CompareTag("Player")){
			manager.CheckpointReached ((GetComponent<Transform>()).position,
				Quaternion.Euler (0,180,0));
		}
	}
}
