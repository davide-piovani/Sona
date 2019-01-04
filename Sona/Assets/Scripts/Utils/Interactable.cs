
using System;
using UnityEngine;
using TMPro;

public class Interactable : MonoBehaviour {

    //radius in which player can interact
    public float radius = 4f;

    bool isFocus = false;
    bool hasInteracted = false;

    GameController gameManager;
    TextMeshPro text;

    public GameObject player;

    /**
     * virtual method
     */
    public virtual void Interact()
    {

    }


    public void Start ()
    {
        //gameManager = FindObjectOfType<GameController>();
        text = GetComponentInChildren<TextMeshPro>();
    }


    public void Update()
    {
        //float distance = Vector3.Distance(gameManager.GetActivePlayer().transform.position, this.transform.position);
        float distance = Vector3.Distance(player.transform.position, this.transform.position);

        if (Input.GetButtonDown("Interact Button") && !hasInteracted)

        {
            if (distance <= radius)
            {
                hasInteracted = true;
                Interact();
            }
        }
        //show text if player is close to the item and item haven't interacted yet 
        if (distance <= radius)
        {
            ShowTooltip();
        }
        
        else
        {
            HideTooltip();
        }
        
    }

    //focus method
    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        //player = playerTransform;
        hasInteracted = false;
    }

    /**
     * Draw interaction radius
     */
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, radius);
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
        var CamPos = Camera.main.transform.position + Camera.main.transform.forward;
        text.enabled = true;
        text.transform.position = CamPos;
        text.transform.localScale = Vector3.one * 0.025f;
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
