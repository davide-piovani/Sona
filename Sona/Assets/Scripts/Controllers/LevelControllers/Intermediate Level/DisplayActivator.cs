using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayActivator : MonoBehaviour {

    //public float radius;
    //GameController _gameController;
    InteractiveTextScript _text;
    GameObject _player;
    bool active = false;
    bool necessary = true;

    void Start()
    {
        //_gameController = GetComponentInParent<GameController>();
        _text = GetComponentInChildren<InteractiveTextScript>();
    }

    void Update()
    {
        //Control();
        if (active & necessary)
        {
            _text.ShowText();
        }
        else {
            _text.HideText();
        }
    }

    public bool IsActive() {
        return active;
    }

    public void Necessary(bool cond) {
        necessary = cond;
    }

    public GameObject GetPlayer() {
        return _player;
    }


    /* da cancellare una volta sistemato il game controller */
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") & other.gameObject.GetComponentInChildren<Camera>().enabled == true /*player is active*/) {
            active = true;
            _player = other.gameObject;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) {

            if (other.gameObject.GetComponentInChildren<Camera>().enabled == false /*player is not active*/) {
                active = false;
            }
            else {
                active = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            active = false;
            _player = null;
        }
    }
    /* fino a qua*/


    /* e sostituisci con questo */
    /*void Control(){
        float distance = Vector3.Distance(gameController.returnActivePlayer, transform.position);
        if (distance <= radius)
        {
            active = true;
        }
        else {
            active = false;
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }*/
 

}
