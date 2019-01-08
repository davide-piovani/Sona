using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Allarm : MonoBehaviour{

    GameController gameController;
    public float radius = 3f;
    bool allarmActive;
    Light alarmLight;

    public void Start()
    {
        gameController = FindObjectOfType<GameController>();
        alarmLight = GetComponentInChildren<Light>();
        allarmActive = true;
    }

    public void Update()
    {
        float distance = Vector3.Distance(gameController.GetActivePlayer().transform.position, transform.position);
        if (distance <= radius)
        {
            if (gameController.GetActivePlayer().IsVisible() & allarmActive)
            {
                Debug.Log("Allarm is ringing");
                AllarmIsRinging();
            }
        }
    }

    //If player interact with the allarm it rings and call for guards
    public void AllarmIsRinging()
    {
        Debug.Log("You pressed the allarm!");
        EventManager.AllarmRinging();
    }

    public bool getAllarmActivate()
    {
        return allarmActive;
    }

    public void deactiveAllarm()
    {
        alarmLight.enabled = false;
        allarmActive = false;
    }

    /**
 * Draw interaction radius
 */
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, radius);
    }

}
