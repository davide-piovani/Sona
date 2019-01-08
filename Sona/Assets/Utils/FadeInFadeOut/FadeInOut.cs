using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour {

    public Image _img;
    //public Text _txt;
    Animator _imgAnimator;
    //Animator _txtAnimator;
    public TextMeshProUGUI text;
    
    void Start () {
        _imgAnimator = _img.gameObject.GetComponent<Animator>();
        text.text = "";
	}

    public void FadeOut(float speed) {
        _imgAnimator.SetFloat("speed", speed);
        _imgAnimator.SetBool("isFadeOut", true);
    }

    public void FadeIn(float speed) {
        _imgAnimator.SetFloat("speed", speed);
        _imgAnimator.SetBool("isFadeOut", false);
    }

    public void ShowText(string tutorialText) {
        text.enabled = true;
        text.text = tutorialText;
    }

    public Image GetImage() {
        return _img;
    }

}
