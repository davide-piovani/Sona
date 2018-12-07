using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseManager : MonoBehaviour {

    public float velocity = 1f;

    bool goUp;
    float scaleX;
    float scaleY;
    float scaleZ;

    void Start () {
        goUp = false;
	}
	
	void Update () {

        scaleX = gameObject.transform.localScale.x;
        scaleY = gameObject.transform.localScale.y;
        scaleZ = gameObject.transform.localScale.z;
        
        if (goUp & gameObject.transform.localScale.y > 0) {
            gameObject.transform.localScale = new Vector3(scaleX, scaleY - velocity*TimeController.GetDelTaTime(), scaleZ);
        }

        if (!goUp & gameObject.transform.localScale.y < 1)
        {
            gameObject.transform.localScale = new Vector3(scaleX, scaleY + velocity * TimeController.GetDelTaTime(), scaleZ);
        }

        if (gameObject.transform.localScale.y <= 0)
        {
            gameObject.transform.localScale = new Vector3(scaleX, 0, scaleZ);
        }

        if (gameObject.transform.localScale.y >= 1)
        {
            gameObject.transform.localScale = new Vector3(scaleX, 1, scaleZ);
        }

    }

    public void OpenCloseDoor(bool cond) {
        goUp = cond;
    } 
}
