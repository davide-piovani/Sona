using UnityEngine.EventSystems;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{

    public LayerMask movementMask;

    public Interactable focus;

    Camera cam;
    PlayerMotor motor;

    // Use this for initialization
    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        */
        //GetMouseButtonDown(0) is left mouse click
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                //move our player to what we hit
                MoveTo(hit.point);

                //stop focusing any objects
                RemoveFocus();
            }
        }
        //GetMouseButtonDown(1) is right mouse click
        if (Input.GetMouseButtonDown(1))
        {              
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                //check if we hit an interactable
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }
            
        }
    }

    private void RemoveFocus()
    {
        if (focus != null)
        {
            focus.onDefocus();
        }
        focus = null;
        motor.StopFollowingTarget();
    }

    private void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
            {
                focus.onDefocus();
            }
            focus = newFocus;
            motor.FollowTarget(newFocus);
        }

        newFocus.OnFocused(transform);

    }

    private void MoveTo(Vector3 point)
    {
        motor.MoveToPoint(point);
        motor.StopFollowingTarget();
    }
}