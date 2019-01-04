using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAudioController : MonoBehaviour {

    public static BackgroundAudioController instance;
    private AudioSource audioSource;

    private void Awake(){
        if (instance != null && instance != this) Destroy(gameObject);
        else {
            instance = this;
            audioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayBackgroundMusic(AudioClip clip){
        if (audioSource.isPlaying) audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void SetVolume(float volume) {
        audioSource.volume = volume;
    }
}
