using UnityEngine;
using TMPro;


public class Tutorial3 : MonoBehaviour
{

    //radius in which player can interact
    public float radius = 3f;
    private float textSize = 0.05f;

    //bool isFocus = false;
    bool hasInteracted3;

    Key key;
    CellDoor cellDoor;

    GameController gameController;
    TextMeshPro text;

    [HideInInspector] public GameObject player;

    public void Start()
    {
        gameController = FindObjectOfType<GameController>();
        text = GetComponentInChildren<TextMeshPro>();
        hasInteracted3 = false;
        key = FindObjectOfType<Key>();
        cellDoor = FindObjectOfType<CellDoor>();
    }


    public void Update()
    {
        if (getDistanceFromPlayer() <= radius && !cellDoor.getDoorOpen() && !key.getKeyTaken())
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