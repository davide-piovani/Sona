using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Transform LoadingBar;
    [SerializeField] float powerLevelIndicator;
    [SerializeField] Player[] scenePlayers;

    [Header("Audioclips")]
    [SerializeField] AudioClip backgroundMusic;

    private float fullWidth;
    private AudioController audioController;

    // Use this for initialization
    void Start () {
        audioController = AudioController.instance;
        PlayBackgroundMusic();
	}

    private void Update() {
        if (Input.GetKey(KeyCode.Escape)){
            FindObjectOfType<SceneLoader>().LoadStartScene();
        }
    }

    public void UpdatePowerLevelIndicator(float level)
    {
        LoadingBar.GetComponent<Image>().fillAmount = level;
    }

    public Player[] GetScenePlayers() { return scenePlayers; }

    private void PlayBackgroundMusic(){
        audioController.PlayBackgroundMusic(backgroundMusic);
    }
}
