using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastTrapButton : MonoBehaviour {

	public LevelManager manager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnCollisionEnter (Collision collision){
		if (collision.collider.CompareTag("Player")){
			manager.LastTrapTrigger();
		}
	}
}
