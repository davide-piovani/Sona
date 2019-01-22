using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        if (player.name.Equals("Jack") & player.GetComponent<Player>().IsPowerActive()) {
            player.GetComponent<Player>().powerActive = false;
            TimeController.changeTime(false);
        }
        PlayerScriptsActive(player, false);
        PlayerInIdle(player);
    }

    void PlayerInIdle(GameObject player)
    {
        if (player.GetComponent<Animator>().GetBool("isRunning"))
        {
            player.GetComponent<Animator>().SetBool("isRunning", false);
        }
        if (player.GetComponent<Animator>().GetBool("isWalking"))
        {
            player.GetComponent<Animator>().SetBool("isWalking", false);
        }
        if (!player.GetComponent<Animator>().GetBool("isIdle"))
        {
            player.GetComponent<Animator>().SetBool("isIdle", true);
        }
    }

    void PlayerScriptsActive(GameObject _player, bool cond)
    {
        MonoBehaviour[] _playerScripts = _player.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour s in _playerScripts)
        {
            s.enabled = cond;
        }
        
    }

}
