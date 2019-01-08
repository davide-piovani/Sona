using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPoint : MonoBehaviour
{
    DialogHelper dialogHelper;
    GameController gameController;
    bool pointReached;
    float radius = 2f;

    private void Start()
    {
        
        dialogHelper = FindObjectOfType<DialogHelper>();
        gameController = FindObjectOfType<GameController>();
        pointReached = false;
    }

    private void Update()
    {
        if (getDistanceFromPlayer() < radius & !pointReached)
        {
            pointReached = true;
            dialogHelper.TutorialAllarmPointReached();
        }
    }

    /**
    * Draw interaction radius
    */
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    float getDistanceFromPlayer()
    {
        float distance = Vector3.Distance(gameController.GetActivePlayer().transform.position, transform.position);
        return distance;
    }
}
