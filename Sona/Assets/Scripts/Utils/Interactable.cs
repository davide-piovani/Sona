
using System;
using UnityEngine;

public class Interactable : MonoBehaviour {

    //radius in which player can interact
    public float radius = 4f;

    bool isFocus = false;
    bool hasInteracted = false;

    Transform player;

    public Transform interactionTransform;

    public virtual void Interact()
    {

    }

    public void Update()
    {
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance <= radius)
            {
                Interact();
                hasInteracted = true;
            }
        }
    }

    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    private void OnDrawGizmosSelected()
    {

        if (interactionTransform == null)
        {
            interactionTransform = transform;
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }

    internal void onDefocus()
    {
        isFocus = false;
        hasInteracted = false;
    }
}
