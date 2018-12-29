using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ApplicationConstants;

public class Jack : Player {

    protected override void LoadPowerSettings(){
        powerDuration = PlayersConstants.jackPowerDuration;
        rechargeSpeed = PlayersConstants.jackRechargeSpeed;
    }

    protected override void PowerToggle(bool isActive){
        powerActive = isActive;
        TimeController.changeTime(isActive);
    }

}
