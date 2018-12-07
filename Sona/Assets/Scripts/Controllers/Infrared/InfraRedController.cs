using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfraRedController : MonoBehaviour {

    public GameObject checkPoint;

    bool infraRedActive;
    bool infraRedActivated;
    InfraRedScript[] _infraReds;
    RayController[] _infraRedsMovement;

    void Start () {
        infraRedActive = false;
		infraRedActivated = false;
        _infraReds = GetComponentsInChildren<InfraRedScript>();
        _infraRedsMovement = GetComponentsInChildren<RayController>();

    }
	
	void Update () {

        if (infraRedActive & infraRedActivated) {
            infraRedActivated = false;
            foreach (InfraRedScript i in _infraReds) {
                i.ActiveInfraRed();
            }
        }
	}

    public void ActiveInfraReds() {
        infraRedActive = true;
        infraRedActivated = true;
    }

    public void ActiveInfraRedsMovement()
    {
        foreach (RayController r in _infraRedsMovement) {
            r.ActiveInfraRedMovement();
        }
    }

    public GameObject GetCheckPoint() {
        return checkPoint;
    }
}
