using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class Door : Interactable {

    public bool locked;
    private bool open;
    private Vector3 fixedPoint;
    private Vector3 rotation = new Vector3 (0,90,0);
    public Level3Manager manager;
    private bool active = false;
    //private NavMeshSurface surface;

    public override void Interact(){
            print ("interacting");
            if (!locked) {
                //GetComponent<Transform>().RotateAround (fixedPoint, Vector3.up, -90);
                GetComponent<Transform>().Rotate (rotation * -1f);
                gameObject.layer = 13;
                open = true;
                manager.EraseMessage();
                //surface.BuildNavMesh();
            } else {
                resetInteraction();
            }
    }

    void Awake () {
        /*Transform trasform = GetComponent<Transform>();
        Vector3 dimensions =GetComponent<Collider>().bounds.size;
        float x;
        float y;
        float z;

        y = transform.position.y;
        x = transform.position.x + dimensions.x / 2 * transform.right.x;
        z = transform.position.z + dimensions.z / 2 * transform.forward.z;

        fixedPoint = new Vector3 (x,y,z);
        open = false;
        print ("fixed point: " + fixedPoint);*/
        radius = 2;
        //surface = FindObjectOfType<NavMeshSurface>();
    }

    public void SetManager (Level3Manager manager){
        this.manager = manager;
    }

    protected override void ShowTooltip (){
        active = true;
        if (!locked && !open){
            manager.ShowMessage("Door nearby. Press b to open", 0);
        }
    }

    protected override void HideTooltip() {
        if(active){
            manager.EraseMessage();
            active = false;
        }
    }
}
