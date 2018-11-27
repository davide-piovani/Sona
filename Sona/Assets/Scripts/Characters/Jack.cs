using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ApplicationConstants;

public class Jack : Player {

    protected override void LoadPowerSettings(){
        powerDuration = GameConstants.jackPowerDuration;
        rechargeSpeed = GameConstants.jackRechargeSpeed;
    }

    protected override void PowerToggle(bool isActive){

    }

}
