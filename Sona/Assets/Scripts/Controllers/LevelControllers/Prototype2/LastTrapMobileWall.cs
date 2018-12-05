using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastTrapMobileWall : MonoBehaviour {

	private const float speed = 2f;
	private const float dest = 23.08f;
	private bool opening = false;

	private Transform tr;

	// Use this for initialization
	void Start () {
		tr = (GetComponent<Transform>());
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 position;
		if (opening && tr.position.x < 23f){
			position = new Vector3 (tr.position.x + speed + Time.deltaTime, tr.position.y, tr.position.z);
			tr.position = position;
		}
	}

	public void Open (){
		this.opening = true;
	}
}
