using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurbineController : MonoBehaviour {

    Animator _animatorController;
    float deltaTime;
    public Transform checkPoint;
    
    void Start () {
        _animatorController = GetComponent<Animator>();	
	}
	
	void Update () {

        deltaTime = TimeController.GetDelTaTime();
        _animatorController.SetFloat("speed", deltaTime);
		
	}

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.position = new Vector3(checkPoint.transform.position.x, 1.3f, checkPoint.transform.position.z);
        }
    }
}
