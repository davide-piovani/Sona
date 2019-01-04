using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffects : MonoBehaviour {

    public AudioClip footstep;
    public AudioClip menuButtonClicked;

    public static AudioEffects instance;
    private AudioSource audioSource;

    private void Awake(){
        AudioEffects[] audioEffectsSources = FindObjectsOfType<AudioEffects>();
        if (audioEffectsSources.Length > 1) Destroy(gameObject);
        else {
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            instance = this;
        }
    }

    public static void PlaySound(AudioClip clip){
        float volume = 1f;
        GameController controller = FindObjectOfType<GameController>();
        if (controller) volume = controller.GetEffectsVolume();
        print("Riproduco clip: " + clip.name + ",   " + volume);
        instance.audioSource.PlayOneShot(clip, volume);
    }
}
