using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameController : MonoBehaviour {

    private float fullHeight;
    [SerializeField] RectTransform powerLevelIndicator;

	// Use this for initialization
	void Start () {
        RectTransform powerLevelIndicatorContainer = (UnityEngine.RectTransform) powerLevelIndicator.parent;
        fullHeight = powerLevelIndicatorContainer.rect.height;
	}
	
    public void UpdatePowerLevelIndicator(float level){
        float newHeight = fullHeight * level;
        powerLevelIndicator.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, newHeight);
    }
}
