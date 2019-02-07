using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractController : MonoBehaviour {

	public float radius = 2f;
    public bool press = true;
    GameController _gameController;
    InteractiveText _text;
    bool active = false;
    bool necessary = true;
    bool forceDeactive = false;
    int state = 0;


    void Start()
    {
        _gameController = FindObjectOfType<GameController>();
        _text = FindObjectOfType<InteractiveText>();
    }

    void Update()
    {
        
        if (!forceDeactive)
        {
            Control();

            if (state == 0)
            {
                if (active & necessary)
                {
                    if (_text.OwnText(press))
                    {
                        state = 1;
                    }
                }
            }
            else if (state == 1)
            {
                if (!active)
                {
                    if (_text.DeOwnText())
                    {
                        state = 0;
                    }
                }
                else if (!necessary)
                {
                    _text.DelText();
                    state = 2;
                }
            }
            else if (state == 2)
            {
                if (necessary)
                {
                    _text.SetText(press);
                    state = 1;
                }
                else if (!active)
                {
                    if (_text.DeOwnText())
                    {
                        state = 0;
                    }
                }
            }
        }
        
    }

    public bool IsActive() {
        return active;
    }

    public void Necessary(bool cond) {
        necessary = cond;
    }

    public GameObject GetPlayer() {
        return _gameController.GetActivePlayer().gameObject;
    }

    public void ReActive() {
        forceDeactive = false;
    }

    public void ForceDeActive() {
        forceDeactive = true;
        active = false;
        _text.DeOwnText();
        state = 0;
    }

    void Control(){
        if (_gameController.isGamePaused()) {
            active = false;
            return;
        }
        Vector3 pos = transform.position;
        float distance = Vector3.Distance(_gameController.GetActivePlayer().transform.position, pos);
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
    }
 
}
