using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PudController : MonoBehaviour {

    Animator _animatorController;
    float deltaTime;
    
    void Start()
    {
        _animatorController = GetComponent<Animator>();
    }

    void Update()
    {

        deltaTime = TimeController.GetDelTaTime();
        _animatorController.SetFloat("speed", deltaTime);

    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("MORTOOOOOOOOOOO " + gameObject.name);
        }
    }
}
