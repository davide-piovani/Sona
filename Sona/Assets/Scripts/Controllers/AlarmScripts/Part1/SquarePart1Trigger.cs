using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquarePart1Trigger : MonoBehaviour {

    GameObject checkPoint;

    void Start () {
        checkPoint = GetComponentInParent<AlarmScheme>().GetCheckpoint();
    }

    void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag("Player") & gameObject.GetComponent<Renderer>().material.color.GetHashCode() == 549295548) {
            other.gameObject.transform.position = new Vector3(checkPoint.transform.position.x, 1.3f, checkPoint.transform.position.z);
        }
        
    }

    void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("Player") & gameObject.GetComponent<Renderer>().material.color.GetHashCode() == 549295548)
        {
            other.gameObject.transform.position = new Vector3(checkPoint.transform.position.x, 1.3f, checkPoint.transform.position.z);
        }

    }
}
