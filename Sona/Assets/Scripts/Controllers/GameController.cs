using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Transform LoadingBar;
    private float fullWidth;
    [SerializeField] float powerLevelIndicator;
    [SerializeField] Player[] scenePlayers;

    // Use this for initialization
    void Start () {
        //RectTransform powerLevelIndicatorContainer = (UnityEngine.RectTransform) powerLevelIndicator.parent;

        //fullWidth = powerLevelIndicatorContainer.rect.width;
	}

    private void Update() {
        if (Input.GetKey(KeyCode.Escape)){
            FindObjectOfType<SceneLoader>().LoadStartScene();
        }
    }

    //public void UpdatePowerLevelIndicator(Player player)
    public void UpdatePowerLevelIndicator(float level)
    {
        /*
        if (player.powerActive)
        {
            player.powerTimeLeft -= Time.deltaTime;

            if (player.powerTimeLeft <= 0) player.PowerToggle(false);
        }
        else
        {
            if (player.powerTimeLeft < player.powerDuration)
            {
                player.powerTimeLeft += Time.deltaTime * player.rechargeSpeed;
            }
            else
            {
                player.powerTimeLeft = player.powerDuration;
            }
        }
        
        if (powerLevelIndicator < 100)
        {
            powerLevelIndicator += level;
        }
        else
        {
            powerLevelIndicator -= level;
        }
        */
        //powerLevelIndicator =  player.powerTimeLeft / player.powerDuration;
        LoadingBar.GetComponent<Image>().fillAmount = level;
    }

    public Player[] GetScenePlayers() { return scenePlayers; }
}
