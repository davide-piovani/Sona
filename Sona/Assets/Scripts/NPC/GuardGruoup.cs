using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardGruoup : MonoBehaviour {

    GuardController[] guards;
    public GameObject allarm;
    public GameObject player;

    void Start() {
        guards = this.GetComponentsInChildren<GuardController>();
    }

    public void GoTo(Transform transform)
    {
        for (int i = 0; i < guards.Length; i++)
        {
            guards[i].MoveTo(transform);
        }
    }

    void OnEnable()
    {
        EventManager.AllarmIsActivatedMethods += MoveToAllarm;
        EventManager.GuardIsCallingMethods += CallOtherGuards;
    }

    void OnDisable()
    {
        EventManager.AllarmIsActivatedMethods -= MoveToAllarm;
        EventManager.GuardIsCallingMethods += CallOtherGuards;
    }

    void MoveToAllarm()
    {
        GoTo(allarm.transform);
    }

    public void CallOtherGuards()
    {
        GoTo(player.transform);
    }

}
