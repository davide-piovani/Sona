using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ApplicationConstants;

public class Hannah : Player {

    [Header("Materials")]
    [SerializeField] Material bodyMaterial;
    [SerializeField] Material hairMaterial;
    [SerializeField] Material transparentBodyMaterial;
    [SerializeField] Material transparentHairMaterial;

    [Header("Renderers")]
    [SerializeField] Renderer bodyRenderer;
    [SerializeField] Renderer hairRenderer;

    public Sprite characterPortrait;

    public override Sprite GetCharacterPortrait()
    {
        return characterPortrait;
    }

    protected override void LoadPowerSettings(){
        powerDuration = PlayersConstants.hannahPowerDuration;
        rechargeSpeed = PlayersConstants.hannahRechargeSpeed;
    }

    protected override void PowerToggle(bool isActive) {
        powerActive = isActive;
        print("Visible: " + !isActive);

        bodyRenderer.material = isActive ? transparentBodyMaterial : bodyMaterial;
        hairRenderer.material = isActive ? transparentHairMaterial : hairMaterial;
    }

}
