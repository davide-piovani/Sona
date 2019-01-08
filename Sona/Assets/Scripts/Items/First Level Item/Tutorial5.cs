using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Tutorial5 : MonoBehaviour
{

    //radius in which player can interact
    public float radius = 3f;
    private float textSize = 0.05f;

    bool tutorial2;

    GameController gameController;
    TextMeshPro text;
    Allarm allarm;

    [HideInInspector] public GameObject player;

    public void Start()
    {
        gameController = FindObjectOfType<GameController>();
        text = GetComponentInChildren<TextMeshPro>();
        allarm = FindObjectOfType<Allarm>();
        tutorial2 = false;
    }


    public void Update()
    {
        int i = 0;
        if (getDistanceFromPlayer() <= radius)
        {
            foreach (Player player in gameController.GetScenePlayers())
            {
                float distance = Vector3.Distance(player.transform.position, transform.position);
                if (distance < radius)
                {
                    i++;
                }
                
            }
            if (i==3)
            {
                //gameController.
            }
        }

        if (getDistanceFromPlayer() <= radius && !allarm.getAllarmActivate())
        {
            ShowTooltip();
        }
        else
        {
            HideTooltip();
        }
    }

    public float getDistanceFromPlayer()
    {
        float distance = Vector3.Distance(gameController.GetActivePlayer().transform.position, transform.position);
        return distance;
    }


    /**
     * Draw interaction radius
     */
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }


    /**
     * This method is used to show the text and to position it on the player.
     */
    protected virtual void ShowTooltip()
    {
        text.enabled = true;
        var playerCam = gameController.GetActivePlayer().gameObject.GetComponentInChildren<Camera>();
        var CamPos = playerCam.transform.position + playerCam.transform.forward;
        text.transform.position = CamPos;
        text.transform.localScale = Vector3.one * textSize;
        text.transform.rotation = playerCam.transform.rotation;


    }

    /**
    * This method is used to hide the text
    */
    protected virtual void HideTooltip()
    {
        text.enabled = false;
    }

}
