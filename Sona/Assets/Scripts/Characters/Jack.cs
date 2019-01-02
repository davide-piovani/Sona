using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ApplicationConstants;

public class Jack : Player {

    public Sprite characterPortrait;

    public override Sprite GetCharacterPortrait()
    {
        return characterPortrait;
    }

    protected override void LoadPowerSettings(){
        powerDuration = PlayersConstants.jackPowerDuration;
        rechargeSpeed = PlayersConstants.jackRechargeSpeed;
    }

    protected override void PowerToggle(bool isActive){
        powerActive = isActive;
        TimeController.changeTime(isActive);
    }

}
