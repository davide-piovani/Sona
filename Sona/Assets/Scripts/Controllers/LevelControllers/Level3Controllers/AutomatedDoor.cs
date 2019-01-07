using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ApplicationConstants;

//This door has two switches and opens both are pressed. If one is released the door closes

public class AutomatedDoor : MonoBehaviour {
    private Vector3 speed;

    //private bool opening = false;
    //private int id = 0;
    //private Switch[2] switches;
    public bool[] triggers = new bool [2];
    private float dist;
    private float dist_done;

    public float time;

    // Use this for initialization
    void Start () {
        triggers[0] = false;
        triggers[1] = false;
        dist = GetComponent<Renderer>().bounds.size.y;
        speed = new Vector3 (0, dist/time, 0);
    }
	
    // Update is called once per frame
    void Update () {
    	if (triggers[1] && triggers[0]){
            if (dist_done < dist){
                transform.position = transform.position + speed * TimeController.GetDelTaTime();
                dist_done = dist_done + speed.y * TimeController.GetDelTaTime();
            }
        } else {
            if (dist_done > 0){
                transform.position = transform.position - speed * TimeController.GetDelTaTime();
                dist_done = dist_done - speed.y * TimeController.GetDelTaTime();
            }
        }
    }

    /*public int Initialize (Switch s){
        //switches[id] = s;
        id = id+1;
        return (id - 1);
    }*/

    public void Open (int id){
        if (id == 0 || id == 1){
            triggers[id] = true;
        }
        print("Called open with id: " + id);
    }

    public void Close (int id){
        if (id == 0 || id == 1){
            triggers[id] = false;
        }
    }
}
