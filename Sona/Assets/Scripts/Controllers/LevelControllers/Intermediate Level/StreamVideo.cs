using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class StreamVideo : MonoBehaviour
{
    public Text pressText;
    public Image pressImage;
    public Text tutorialText;
    public RawImage videoImage;
    public VideoPlayer videoPlayer;
    //public AudioSource audioSource;

    KeyButtonScript _keyButton;

    void Start() {
        _keyButton = FindObjectOfType<KeyButtonScript>();
    }

    public void setVideoSource(VideoClip video) {
        videoPlayer.clip = video;
    }

    public void Play() {
        StartCoroutine(PlayVideo());
    }

    public void ShowMessage(string txt, string button) {
        tutorialText.text = txt;
        if (button.Equals("CHANGE_CHARACTER"))
        {
            tutorialText.rectTransform.position = videoImage.rectTransform.position - new Vector3(0, 250, 0);
        }
        if (button.Equals("ACTIVE_POWER"))
        {
            tutorialText.rectTransform.position = videoImage.rectTransform.position - new Vector3(0, 250, 0);
        }
        else { }
    }

    public void EraseMessage()
    {
        tutorialText.text = "";
    }

    public void SetPressText(string txt, string button)
    {
        pressText.text = txt;
        if (button.Equals("CHANGE_CHARACTER")) {
            pressImage.rectTransform.position = pressText.rectTransform.position + new Vector3(-90, 1, 0);
            pressImage.overrideSprite = _keyButton.GetChangeCharacterButtonImage();
        }
        if (button.Equals("ACTIVE_POWER"))
        {
            pressImage.rectTransform.position = pressText.rectTransform.position + new Vector3(-75, 1, 0);
            pressImage.overrideSprite = _keyButton.GetActivePowerButtonImage();
        }
        else {
            pressImage.sprite = null;
        }

    }

    public void ErasePressText()
    {
        pressImage.sprite = null;
        pressText.text = "";
    }

    IEnumerator PlayVideo()
    {
        videoPlayer.Prepare();
        videoImage.color = new Vector4(videoImage.color.r, videoImage.color.g, videoImage.color.b, 0);
        WaitForSeconds waitForSeconds = new WaitForSeconds(1);
        while (!videoPlayer.isPrepared)
        {
            yield return waitForSeconds;
            break;
        }
        videoImage.texture = videoPlayer.texture;
        videoImage.color = new Vector4(videoImage.color.r, videoImage.color.g, videoImage.color.b, 1);
        videoPlayer.Play();
        //audioSource.Play();
    }
}
