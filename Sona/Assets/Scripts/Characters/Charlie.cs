using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using ApplicationConstants;

public class Charlie : Player {
    private const float n_heigth = 1.1f;
    private const float c_height = 0.55f;

    private bool crouching = false; 

    protected override void LoadPowerSettings(){
        powerDuration = PlayersConstants.charliePowerDuration;
        rechargeSpeed = PlayersConstants.charlieRechargeSpeed;
        //r_speed = 2f;
    }

    protected override void PowerToggle(bool isActive){
        powerActive = isActive;
        Physics.IgnoreLayerCollision(PlayersConstants.playerLayer, PlayersConstants.dashableObjectsLayer, powerActive);
        Debug.Log("Collisions: " + (!powerActive).ToString());

        if (isActive){
	    layerMask = layerMask & ~(1 << 10);
	} else {
	    layerMask = layerMask | 1 << 10;
	}
    }

    public bool Crouch (){
        CameraController camera;
        if(!crouching){
            camera = GetComponentInChildren<CameraController>();
            crouching = true;
            DisableInput();
            camera.MoveToFirst();
            gameController.ChangePlayerActive (false);
            print("CHARLIE: Begun movement");
        } else if (characterCamera.transform.position == (gameObject.transform.position + playerCollider.center)){
            ActiveInput();
            SkinnedMeshRenderer renderer = GetComponentInChildren<SkinnedMeshRenderer>();
            GetComponent<NavMeshAgent>().enabled = false;
            renderer.enabled = false;
            playerCollider.height = c_height;
            gameObject.transform.position = gameObject.transform.position - new Vector3 (0,0.55f,0);
            print("CHARLIE: EndPosition: " + gameObject.transform.position);
            print("CHARLIE: End movement");
            return (true);
        }
        return (false);
    }

    public bool Stand (){
        //Vector3 startRay = gameObject.transform.position + new Vector3(0, 0.5f, 0);
        //Debug.DrawRay(startRay, Vector3.down, Color.white, Mathf.Infinity);
        //if(!Physics.Raycast(startRay, Vector3.down, Mathf.Infinity, 1<<15)){
            CameraController camera = GetComponentInChildren<CameraController>();
            SkinnedMeshRenderer renderer = GetComponentInChildren<SkinnedMeshRenderer>();
            renderer.enabled = true;
            playerCollider.height = n_heigth;
            GetComponent<NavMeshAgent>().enabled = true;
            camera.MoveToThird();
            gameController.ChangePlayerActive(true);
            crouching = false;
        //}
        return true;
    }
}
