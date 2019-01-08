using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FadeTutorials : MonoBehaviour {

    public TextMeshProUGUI[] texts;

    public Image _img;
    //public Text _txt;
    Animator _imgAnimator;

    void Start()
    {
        _imgAnimator = _img.gameObject.GetComponent<Animator>();
    }

    public void FadeOut(float speed)
    {
        _imgAnimator.SetFloat("speed", speed);
        _imgAnimator.SetBool("isFadeOut", true);
    }

    public void FadeIn(float speed)
    {
        _imgAnimator.SetFloat("speed", speed);
        _imgAnimator.SetBool("isFadeOut", false);
    }

    public void ShowText( int i)
    {
        for (int j = 0; j <= texts.Length; j++)
        {
            if (j != i)
            {
                texts[j].enabled = false;
            }
            else
            {
                texts[j].enabled = true;
            }
        }
    }

    public void deactiveAllTexts()
    {
        for (int j = 0; j < texts.Length; j++)
        {
            texts[j].enabled = false;
        }
    }

    public Image GetImage()
    {
        return _img;
    }
}
