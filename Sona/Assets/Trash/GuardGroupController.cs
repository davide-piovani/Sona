using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardGroupController : MonoBehaviour {

    GuardController[] guards;

    GuardState state;
    //GuardController[] guards;

    float lookRadius;

    Transform target;

    // Use this for initialization
    void Start () {
        guards = this.GetComponentsInChildren<GuardController>();
    }

    // Update is called once per frame
    void Update()
    {
        bool playerDetected = false;
        float distance;
        //in this for cycle playerDetected
        for (int i = 0; i < guards.Length; i++)
        {
            distance = DetectPlayer(guards[i]);
            if (distance < 0)
            {
                playerDetected = true;
            }
        }
        //if player is detected all guards of the group will move to him.
        if (playerDetected)
        {
            for (int i = 0; i < guards.Length; i++)
            {
                //guards[i].SetUpTarget(target);
            }
        }

    }

    /**
     * This method is used to change guard state
     */
    public void changeState(GuardState newState)
    {
        state = newState;
        lookRadius = state.GetRadius();
        Debug.Log(lookRadius);
    }

    /*
    bool InLineOfSight()
    {
    // Parent an empty gameobject called eye and
    // place it at the eye of the AI object with the z axis facing forward
    Physics.Linecast(eye.transform.position, eye.transform.position + (eye.transform.forward* viewDistance), out hitInfo );
    // Test out by making the rigidbody isKinametic = true and manually dragging the AI to front of player/ good AI
    Debug.Log(hit.collider.tag);
    return true;
    }
    */

    /**
     * This method is used 
     * @return  distance positive if a guard detect the player
     *          distance negative if player is not detected
     */
    float DetectPlayer(GuardController guard)
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            return distance;
        }
        return -1;
    }

    /**
     * This method is used when a guard reachs the player. Player was catch and the game ends
     */
    void EndGame()
    {
        Debug.Log("Guard reach the player, you lose!");
    }


}
