using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogHelper : MonoBehaviour {

    public TextMeshProUGUI[] texts;
    GameController gameController;
    public Image background;
    public GameObject point1;
    //CellDoor cellDoor;
    public float waitTimeText = 6f;

    private float textSize = 0.05f;

    bool dialogHelperActive;
    int i;

    // Use this for initialization
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        dialogHelperActive = false;
        i = 0;
        //cellDoor = FindObjectOfType<CellDoor>();
        StartCoroutine(InitialTutorial());
    }

    private void showTutorialText()
    {
        //background.enabled = true;
        for (int j = 0; j <= i; j++)
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

    IEnumerator InitialTutorial()
    {
        dialogHelperActive = true;
        i = 0;
        showTutorialText();
        yield return new WaitForSeconds(waitTimeText);
        i++;
        showTutorialText();
        yield return new WaitForSeconds(waitTimeText);
        i++;
        showTutorialText();
        yield return new WaitForSeconds(waitTimeText);
        dialogHelperActive = false;
        deactiveAllTexts();

    }

    IEnumerator PointReached()
    {
        showTutorialText();
        yield return new WaitForSeconds(waitTimeText);
        deactiveAllTexts();
    }

    public void TutorialDoorOpenPointReached()
    {
        dialogHelperActive = true;
        i = 3;
        StartCoroutine(PointReached());
        dialogHelperActive = false;
    }

    public void TutorialAllarmPointReached()
    {
        dialogHelperActive = true;
        i = 4;
        StartCoroutine(PointReached());
        dialogHelperActive = false;
    }

    void deactiveAllTexts()
    {
        for (int j = 0; j < texts.Length; j++)
        {
            texts[j].enabled = false;
        }
        //background.enabled = false;
    }

    public bool dialogHelperIsActive()
    {
        return dialogHelperActive;
    }

    public void setActive()
    {
        dialogHelperActive = true;
    }

    public void setNotActive()
    {
        dialogHelperActive = false;
    }

}
/**
public TextMeshProUGUI[] texts;
public TextMeshProUGUI text;
//GameController gameController;
public Image background;
public GameObject point1;
public float waitTimeText = 6f;
//FadeTutorials fade;
FadeInOut fade;


bool dialogHelperActive;
int i;

// Use this for initialization
void Start () {
    fade = FindObjectOfType<FadeInOut>();
    //fadeInOut = FindObjectOfType<FadeInOut>();
    //gameController = FindObjectOfType<GameController>();
    dialogHelperActive = false;
    i = 0;
    StartCoroutine(InitialTutorial());
}

private void showTutorialText()
{
    text.enabled = true;
    for (int j = 0; j < texts.Length; j++)
    {
        if (j != i)
        {
            //text.text = "";
            //fade.ShowText("");
            texts[j].enabled = false;
        }
        else
        {
            texts[j].enabled = false;
            //fade.ShowText(texts[j].text);
            //text.text = texts[j].text;
            //Debug.Log(text.text);
        }
    }
} 

IEnumerator InitialTutorial()
{
    dialogHelperActive = true;
    i = 0;
    showTutorialText();
    yield return new WaitForSeconds(waitTimeText);
    deactiveAllTexts();

    
    i++;
    showTutorialText();
    yield return new WaitForSeconds(waitTimeText);
    deactiveAllTexts();
    i++;
    showTutorialText();
    yield return new WaitForSeconds(waitTimeText);
    deactiveAllTexts();
    dialogHelperActive = false;

    /*
    dialogHelperActive = true;
    i = 0;
    yield return new WaitForSeconds(2);
    fade.FadeOut(1);
    yield return new WaitUntil(() => fade.GetImage().color.a == 1);
    showTutorialText();
    yield return new WaitForSeconds(waitTimeText);
    i++;
    showTutorialText();
    yield return new WaitForSeconds(waitTimeText);
    i++;
    showTutorialText();
    yield return new WaitForSeconds(waitTimeText);
    dialogHelperActive = false;
    deactiveAllTexts();
    text.text = "";
    fade.FadeIn(1);
    */
/**
}

IEnumerator PointReached()
{
showTutorialText();
yield return new WaitForSeconds(waitTimeText);
deactiveAllTexts();
}

public void TutorialDoorOpenPointReached()
{
dialogHelperActive = true;
i = 3;
StartCoroutine(PointReached());
dialogHelperActive = false;
}

public void deactiveAllTexts()
{
for (int j = 0; j < texts.Length; j++)
{
    texts[j].enabled = false;
}
}

public void TutorialAllarmPointReached()
{
dialogHelperActive = true;
i = 4;
StartCoroutine(PointReached());
dialogHelperActive = false;
}

public bool dialogHelperIsActive()
{
return dialogHelperActive;
}


}
*/
