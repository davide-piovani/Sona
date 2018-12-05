using UnityEngine;
using ApplicationConstants;

public class Charlie : Player {


    protected override void LoadPowerSettings(){
        powerDuration = GameConstants.charliePowerDuration;
        rechargeSpeed = GameConstants.charlieRechargeSpeed;
    }

    protected override void PowerToggle(bool isActive){
        powerActive = isActive;
        Physics.IgnoreLayerCollision(GameConstants.playerLayer, GameConstants.dashableObjectsLayer, powerActive);
        Debug.Log("Collisions: " + (!powerActive).ToString());
    }
}
