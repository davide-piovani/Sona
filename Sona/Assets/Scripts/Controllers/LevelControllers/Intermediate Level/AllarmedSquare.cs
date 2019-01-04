using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllarmedSquare : MonoBehaviour {

    bool safe = false;

    public void Safe(bool cond) {
        safe = cond;
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player") & !safe)
        {
            Debug.Log("MORTO");
        }

    }

    void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("Player") & !safe)
        {
            Debug.Log("MORTO");
        }

    }
}
