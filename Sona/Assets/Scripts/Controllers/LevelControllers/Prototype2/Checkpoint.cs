using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

	public LevelManager manager;
	public float y_rotation;
	private bool used = false;
	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnCollisionEnter (Collision other){
		if (other.collider.CompareTag("Player") && !used){
			manager.CheckpointReached ((GetComponent<Transform>()).position,
				Quaternion.Euler(0, y_rotation, 0));
		}
	}
}
