using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardGruoup : MonoBehaviour {

    GuardController[] guards;
    public GameObject allarm;
    public GameObject player;
    public InitialGuardState initialGuardGroupState;
    GuardState state, currentState;
    [HideInInspector] public Action initialState;

    /**
     * This enum is used to set the initial state for guard group
     */
    public enum InitialGuardState
    {
        sleep,
        investigate,
        allert,
        relax
    }

    /**
     * Method used at the start, when object is created
     */
    void Start() {
        guards = GetComponentsInChildren<GuardController>();
        initialState = setState(initialGuardGroupState);
    }


    /**
     * Those methods are used to sign up to event manager methods
     */
    void OnEnable()
    {
        EventManager.AllarmIsActivatedMethods += MoveToAllarm;
        EventManager.GuardIsCallingMethods += CallOtherGuards;
        //EventManager.StopFollowPlayerMethods += StopFollowPlayer;
        //EventManager.ChangeStateMethods += ChangeStateGuards;
        //EventManager.ChangeInInvestigationStateMethods += ChangeAllertState;
        //EventManager.ChangeInPatrollingStateMethods += ChangeInvestigationState;
    }

    void OnDisable()
    {
        EventManager.AllarmIsActivatedMethods -= MoveToAllarm;
        EventManager.GuardIsCallingMethods -= CallOtherGuards;
        //EventManager.StopFollowPlayerMethods -= StopFollowPlayer;
        //EventManager.ChangeStateMethods -= ChangeStateGuards;
        //EventManager.ChangeInInvestigationStateMethods -= ChangeAllertState;
        //EventManager.ChangeInPatrollingStateMethods -= ChangeInvestigationState;
    }

    /**
     * This method is used to set the initial guard group state
     */
    public Action setState(InitialGuardState state)
    {
        Action currentAction = null;
        int i;
        switch (state)
        {
            case InitialGuardState.sleep:
                {
                    currentAction = new Sleep();
                    break;
                }
            case InitialGuardState.allert:
                {
                    currentAction = new LookingForSomeone();
                    break;
                }
            case InitialGuardState.investigate:
                {
                    currentAction = new Patrolling();
                    break;
                }
            case InitialGuardState.relax:
                {
                    currentAction = new Relax();
                    break;
                }
        }
        if (currentAction == null)
        {
            Debug.Log("Azione iniziale è null!! ");
        }
        return currentAction;
    }



    void ChangeAction(int i)
    {
        //currentState = new AllertState();
        //ChangeStateGuards(currentState);

    }
    /*
    void ChangeInvestigationState()
    {
        for (int i = 0; i < guards.Length; i++)
        {
            guards[i].Patrolling();
            Debug.Log("Guards changed state in: " + state.name);
        }
        //currentState = new InvestigateState();
        //ChangeStateGuards(currentState);
    }
    
    void ChangeStateGuards(GuardState state)
    {
        for (int i = 0; i < guards.Length; i++)
        {
            guards[i].ChangeState(state);
            Debug.Log("Guards changed state in: " + state.name);
        }
    }
    */
    /**
     * Those methods are used to make guards go to a point/ to the allarm linked with the group and to call other guards in case of someone spotted the player
     */
    public void GoTo(Transform transform)
    {
        for (int i = 0; i < guards.Length; i++)
        {
            //guards[i].MoveTo(transform);
            guards[i].target = transform;
            guards[i].setAction(new Chase());
        }
    }

    void MoveToAllarm()
    {
        GoTo(allarm.transform);
    }

    public void CallOtherGuards()
    {
        GoTo(player.transform);
    }

    public void StopFollowPlayer()
    {
        for (int i = 0; i < guards.Length; i++)
        {
            //guards[i].MoveTo(transform);
            guards[i].StopFollow(guards[i].target);
            guards[i].setAction(new LookingForSomeone());
        }
    }

}
