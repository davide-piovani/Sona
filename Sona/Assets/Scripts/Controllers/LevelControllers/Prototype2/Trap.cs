using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {

	public LevelManager manager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnCollisionStay (Collision other){
		if (other.collider.CompareTag ("Player")){
			manager.PlayerDies();
		}
	}
}
