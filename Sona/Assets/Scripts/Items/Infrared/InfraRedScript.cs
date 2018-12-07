using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfraRedScript : MonoBehaviour {

    GameObject checkPoint;
    LineRenderer line;
    bool active;

    void Start () {
        line = gameObject.GetComponent<LineRenderer>();
        checkPoint = GetComponentInParent<InfraRedController>().GetCheckPoint();
        active = false;
        line.enabled = false;
	}
	
	void Update () {

	}

    public void ActiveInfraRed() {
        active = true;
        StopCoroutine("LaserActive");
        StartCoroutine("LaserActive");
    }

    IEnumerator LaserActive() {

        line.enabled = true;

        while (active) {

            Ray ray = new Ray(transform.position, transform.up);
            RaycastHit hit;

            line.SetPosition(0, ray.origin);

            if (Physics.Raycast(ray, out hit, 100)) {
                line.SetPosition(1, hit.point);
                if (hit.transform.gameObject.CompareTag("Player")) {
                    hit.transform.position = new Vector3(checkPoint.transform.position.x, 1.3f, checkPoint.transform.position.z);
                }
            }
            else {
                line.SetPosition(1, ray.GetPoint(100));
            }

            yield return null;
        }

        active = false;
        line.enabled = false;
        
    }
}
