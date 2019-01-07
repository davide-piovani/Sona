using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour {

    public GameObject cam;

    private Light torchLight;
    private Animator anim;
    private Renderer torchRenderer;
    private float base_intensity;
    private Vector3 w_position;
    private Vector3 i_position;
    private Transform tr;
    private Transform parent;

    // Use this for initialization
    void Start () {
        Transform main;

        tr = GetComponent<Transform>();
        parent = tr.parent;
        torchLight = GetComponentInChildren<Light>();
        torchRenderer = GetComponent<Renderer>();
        main = parent.parent;
        anim = main.gameObject.GetComponent<Animator> ();

        w_position = new Vector3 (0.00153f, -0.00114f, 0.00025f);
        i_position = new Vector3 (0.00218f, -0.00083f, -0.00063f);

        if (torchLight == null || anim == null || torchRenderer == null){
            print ("Missing fundamental component");
            Destroy (gameObject);
        }
        base_intensity = torchLight.intensity;	
    }
	
    // Update is called once per frame
    void Update () {
        if (anim.GetBool ("isWalking")){
            tr.localPosition = w_position;
            torchLight.intensity = base_intensity;
            torchRenderer.enabled = true;
            //RotateLight();
        } else if (anim.GetBool("isIdle")){
            tr.localPosition = i_position;
            torchLight.intensity = base_intensity;
            torchRenderer.enabled = true;
            //RotateLight();
        } else {
            torchLight.intensity = 0f;
            torchRenderer.enabled = false;
        }
    }

    /*void RotateLight (){
        float angle;
        Vector3 start = parent.right;
        Vector3 dest = cam.transform.forward;

        dest[2] = 0;

        angle = Vector3.SignedAngle(start, dest, Vector3.up);
        if (Mathf.Abs(angle) > 30){
            angle = 30 * Mathf.Sign(angle);
        }
        tr.localRotation = Quaternion.Euler(0, 0, 180 + angle);
    }*/
}
