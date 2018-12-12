using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

	private int ID = 0;
	private LevelManager manager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetData (int id, LevelManager manager){
		ID = id;
		this.manager = manager;
	}

	public void OnCollisionEnter (Collision other){
		if (other.collider.CompareTag ("Player")){
			manager.PlatformTriggered(ID);
		}
	}
}
