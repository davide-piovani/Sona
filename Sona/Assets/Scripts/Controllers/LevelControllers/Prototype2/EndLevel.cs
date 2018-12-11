using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour {

	private LevelManager manager;

	void Awake ()	{
		manager = FindObjectOfType<LevelManager>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	public void OnCollisionEnter (Collision collision) {
		if (collision.collider.CompareTag("Player")){
			manager.BackToMenu();
		}
	}
}
