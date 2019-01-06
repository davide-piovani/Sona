
using System;
using UnityEngine;
using TMPro;

public class Interactable : MonoBehaviour {

    //radius in which player can interact
    public float radius = 4f;
    private float textSize = 0.05f;

    bool isFocus = false;
    bool hasInteracted = false;

    GameController gameController;
    TextMeshPro text;
    DialogHelper dialogHelper;

    [HideInInspector] public GameObject player;

    /**
     * virtual method
     */
    public virtual void Interact()
    {

    }


    public void Start ()
    {
        gameController = FindObjectOfType<GameController>();
        text = GetComponentInChildren<TextMeshPro>();
        dialogHelper = FindObjectOfType<DialogHelper>();
    }


    public void Update()
    {

        if (Input.GetButtonDown("InteractButton") && !hasInteracted)

        {
            if (getDistanceFromPlayer() <= radius)
            {
                hasInteracted = true;
                Debug.Log("Interacted");
                Interact();
            }
        }
        //show text if player is close to the item and item haven't interacted yet 
        if (getDistanceFromPlayer() <= radius && !hasInteracted && !dialogHelper.dialogHelperIsActive())
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
    
    //focus method
    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        hasInteracted = false;
    }
    

    /**
     * Draw interaction radius
     */
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    
    //defocus method
    internal void onDefocus()
    {
        isFocus = false;
        hasInteracted = false;
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

    protected void resetInteraction (){
        hasInteracted = false;
    }
}
