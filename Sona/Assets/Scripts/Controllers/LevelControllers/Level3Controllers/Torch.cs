using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour {

    private Light light;
    private Animator anim;
    private Renderer renderer;
    private float base_intensity;

    // Use this for initialization
    void Start () {
        Transform main;

        light = GetComponentInChildren<Light>();
        renderer = GetComponent<Renderer>();
        main = GetComponent<Transform>().parent.parent;
        anim = main.gameObject.GetComponent<Animator> ();

        if (light == null || anim == null || renderer == null){
            print ("Missing fundamental component");
            Destroy (gameObject);
        }
        base_intensity = light.intensity;	
    }
	
    // Update is called once per frame
    void Update () {
        if (anim.GetBool ("isWalking")){
            light.intensity = base_intensity;
            renderer.enabled = true;
            
        } else {
            light.intensity = 0f;
            renderer.enabled = false;
        }
    }
}
