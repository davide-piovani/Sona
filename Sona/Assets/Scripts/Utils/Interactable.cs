
using System;
using UnityEngine;
using TMPro;

public class Interactable : MonoBehaviour {

    //radius in which player can interact
    public float radius = 4f;

    bool isFocus = false;
    bool hasInteracted = false;

    public GameObject playerObject;
    TextMeshPro text;

    Transform player;

    public Transform interactionTransform;

    /**
     * virtual method
     */
    public virtual void Interact()
    {

    }


    public void Start ()
    {
        text = GetComponentInChildren<TextMeshPro>();
        player = playerObject.transform;
    }


    public void Update()
    {
        float distance = Vector3.Distance(playerObject.GetComponent<Transform>().position, interactionTransform.position);
        if (Input.GetKeyDown ("b") && !hasInteracted)
        {
            if (distance <= radius)
            {
                hasInteracted = true;
                Interact();
            }
        }

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
        player = playerTransform;
        hasInteracted = false;
    }

    /**
     * Draw interaction radius
     */
    private void OnDrawGizmosSelected()
    {

        if (interactionTransform == null)
        {
            interactionTransform = transform;
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
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
