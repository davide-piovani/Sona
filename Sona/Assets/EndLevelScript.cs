using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ApplicationConstants;

public class EndLevelScript : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == PlayersConstants.playerTag)
        {
            FindObjectOfType<SceneLoader>().LoadNextScene();
        }
    }
}
