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
        EventManager.ChangeStateMethods += ChangeStateGuards;
    }

    void OnDisable()
    {
        EventManager.AllarmIsActivatedMethods -= MoveToAllarm;
        EventManager.GuardIsCallingMethods -= CallOtherGuards;
        EventManager.ChangeStateMethods -= ChangeStateGuards;
    }

    void MoveToAllarm()
    {
        GoTo(allarm.transform);
    }

    public void CallOtherGuards()
    {
        GoTo(player.transform);
    }

    public void ChangeStateGuards()
    {
        for (int i = 0; i < guards.Length; i++)
        {
            guards[i].ChangeState(new SleepState());
            Debug.Log("Guards changed state");
        }
    }

}
