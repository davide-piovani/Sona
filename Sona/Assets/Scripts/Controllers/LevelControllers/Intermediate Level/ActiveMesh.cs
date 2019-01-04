using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ActiveMesh : MonoBehaviour {

    public float radius = 2f;
    GameController _gameController;

    void Start()
    {
        _gameController = FindObjectOfType<GameController>();
    }

    void Update()
    {
        Control();
    }

    void Control()
    {
        float distance = Vector3.Distance(_gameController.GetActivePlayer().gameObject.transform.position, transform.position);

        if (distance <= radius * 1.5) {
            if (distance <= radius)
            {
                _gameController.GetActivePlayer().gameObject.GetComponent<NavMeshAgent>().enabled = false;
            }
            else
            {
                _gameController.GetActivePlayer().gameObject.GetComponent<NavMeshAgent>().enabled = true;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }


}
