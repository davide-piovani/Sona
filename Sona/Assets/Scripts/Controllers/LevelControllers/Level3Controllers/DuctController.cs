using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ApplicationConstants;
using UnityStandardAssets.CrossPlatformInput;

public class DuctController : MonoBehaviour {

    private bool active = true;
    private bool operating = false;
    private Level3Manager manager;

    void Start(){
        manager = FindObjectOfType<Level3Manager>();
    }

    void OnTriggerStay (Collider other){
        //Collider other;

        //other = collision.collider;
        if (other.CompareTag(PlayersConstants.playerTag) && active){
            Player player = other.gameObject.GetComponent<Player>();
            if (player.GetPlayerType() == PlayerType.Charlie && player.IsInputActive()){
                manager.ShowMessage("b: crouch", 0);
                //print("DUCT CONTROLLER: considering collision");
                if (CrossPlatformInputManager.GetButtonDown(PlayersConstants.interactButton)){
                    manager.EraseMessage();
                    operating = true;
                    print("DUCTCONTROLLER: interacting");
                }
                if(operating){
                    print ("DUCTCONTROLLER: Operating");
                    Charlie charlie = player as Charlie;
                    operating = !(charlie.Crouch());
                    if (!operating){
                    active = false;
                    }
                }
            } else if (player.IsInputActive()) {
                manager.ShowMessage("Only Charlie can pass through here", 0);
            }
        }
    }

    void OnTriggerExit (Collider other){
        if (other.CompareTag(PlayersConstants.playerTag)){
            manager.EraseMessage();
            Player player = other.gameObject.GetComponent<Player>();
            if (player.GetPlayerType() == PlayerType.Charlie){
                Charlie charlie = player as Charlie;
                charlie.Stand();
            }
            operating = false;
            active = true;
        }
    }
}
