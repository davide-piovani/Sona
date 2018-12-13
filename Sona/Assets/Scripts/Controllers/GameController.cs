using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameController : MonoBehaviour {

    private float fullWidth;
    [SerializeField] RectTransform powerLevelIndicator;
    [SerializeField] Player[] scenePlayers;

    // Use this for initialization
    void Start () {
        RectTransform powerLevelIndicatorContainer = (UnityEngine.RectTransform) powerLevelIndicator.parent;
        fullWidth = powerLevelIndicatorContainer.rect.width;
	}
	
    public void UpdatePowerLevelIndicator(float level){
        float newWidth = fullWidth * level;
        powerLevelIndicator.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newWidth);
    }

    public Player[] GetScenePlayers() { return scenePlayers; }
}
