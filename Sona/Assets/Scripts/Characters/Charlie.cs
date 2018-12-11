using UnityEngine;
using ApplicationConstants;

public class Charlie : Player {


    protected override void LoadPowerSettings(){
        powerDuration = PlayersConstants.charliePowerDuration;
        rechargeSpeed = PlayersConstants.charlieRechargeSpeed;
    }

    protected override void PowerToggle(bool isActive){
        powerActive = isActive;
        Physics.IgnoreLayerCollision(PlayersConstants.playerLayer, PlayersConstants.dashableObjectsLayer, powerActive);
        Debug.Log("Collisions: " + (!powerActive).ToString());
    }
}
