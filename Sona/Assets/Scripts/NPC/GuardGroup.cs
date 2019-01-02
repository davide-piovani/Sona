using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardGroup : MonoBehaviour {

    GuardController[] guards;
    public GameObject allarm;
    private GameObject player;
    public InitialGuardState initialGuardGroupState;
    GuardState state, currentState;
    [HideInInspector] public Action initialState;

    private GameController gameController;       //Da sostituire con GameController

    /*
        Prendi il giocatore attivo da GameController
    */


    /**
     * This enum is used to set the initial state for guard group
     */
    public enum InitialGuardState
    {
        sleep,
        patrol,
        allert,
        relax
    }

    /**
     * Method used at the start, when object is created
     */
    void Start() {
        guards = GetComponentsInChildren<GuardController>();
        initialState = setState(initialGuardGroupState);

        gameController = FindObjectOfType<GameController>();
    }


    /**
     * Those methods are used to sign up to event manager methods
     */
    void OnEnable()
    {
        EventManager.AllarmIsActivatedMethods += MoveToAllarm;
        EventManager.GuardIsCallingMethods += CallOtherGuards;

    }

    void OnDisable()
    {
        EventManager.AllarmIsActivatedMethods -= MoveToAllarm;
        EventManager.GuardIsCallingMethods -= CallOtherGuards;

    }

    /**
     * This method is used to set the initial guard group state
     */
    public Action setState(InitialGuardState state)
    {
        Action currentAction = null;
        switch (state)
        {
            case InitialGuardState.sleep:
                {
                    //currentAction = new Sleep();
                    break;
                }
            case InitialGuardState.allert:
                {
                    currentAction = new LookingForSomeone();
                    break;
                }
            case InitialGuardState.patrol:
                {
                    currentAction = new Patrolling();
                    break;
                }
            case InitialGuardState.relax:
                {
                    //currentAction = new Relax();
                    break;
                }
        }
        if (currentAction == null)
        {
            //Debug.Log("Azione iniziale è null!! ");
        }
        return currentAction;
    }


    /**
     * Those methods are used to make guards go to a point/ to the allarm linked with the group and to call other guards in case of someone spotted the player
     */
    public void GoTo(Transform transform)
    {
        for (int i = 0; i < guards.Length; i++)
        {
            //guards[i].MoveTo(transform);
            guards[i].target = transform;
            //guards[i].setAction(new MovingSomewhere());
        }
    }

    void MoveToAllarm()
    {
        for (int i = 0; i < guards.Length; i++)
        {
            //guards[i].MoveTo(transform);
            //guards[i].target = transform;
            guards[i].setAction(new MovingSomewhere());
        }
    }

    public void CallOtherGuards()
    {
        GoTo(gameController.GetActivePlayer().transform);
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
