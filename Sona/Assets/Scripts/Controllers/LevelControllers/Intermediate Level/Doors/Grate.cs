using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grate : MonoBehaviour {

    Vector3 initPos;
    Vector3 targetPos;
    float speed = 10f;

	void Start () {
        initPos = gameObject.transform.position;
        targetPos = gameObject.transform.position - new Vector3(0, 2.55f, 0);
	}

    public void LockDoor() {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPos, speed * TimeController.GetDelTaTime());
    }

    public void UnlockDoor() {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, initPos, speed * TimeController.GetDelTaTime());
    }
}
