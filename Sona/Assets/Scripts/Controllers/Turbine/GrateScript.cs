using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrateScript : MonoBehaviour {

    Rigidbody rb;
    
    void Start () {

        rb = GetComponent<Rigidbody>();
		
	}
	
	void Update () {

        if (Input.GetKey(KeyCode.B)) {
            rb.AddForce(new Vector3(1f, 0, 0), ForceMode.Impulse);
        }
		
	}
}
