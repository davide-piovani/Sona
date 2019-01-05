using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllarmedSquare : MonoBehaviour {

    AllarmController _allarmController;
    bool safe = false;
    
    void Start() {
        _allarmController = FindObjectOfType<AllarmController>();
    }

    public void Safe(bool cond) {
        safe = cond;
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player") & !safe & !_allarmController.IsEnemyDetect())
        {
            _allarmController.EnemyDetected();
            BlockEnemy(other.gameObject);
        }

    }

    void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("Player") & !safe & !_allarmController.IsEnemyDetect())
        {
            _allarmController.EnemyDetected();
            BlockEnemy(other.gameObject);
        }

    }

    void BlockEnemy(GameObject player) {
        //mettere in Idle il giocatore
        //disattivarne il movimento
        //se ha potere attivo disattivarlo
        /*MonoBehaviour[] _playerScripts = player.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour s in _playerScripts)
        {
            s.enabled = false;
        }*/
    }

}
