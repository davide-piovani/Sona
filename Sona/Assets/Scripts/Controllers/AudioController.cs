using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    public static AudioController instance;
    private AudioSource audioSource;

    private void Awake(){
        if (instance != null && instance != this) Destroy(gameObject);
        else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start(){
        audioSource = GetComponent<AudioSource>(); 
    }

    public void PlayBackgroundMusic(AudioClip clip){
        if (audioSource.isPlaying) audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }
}
