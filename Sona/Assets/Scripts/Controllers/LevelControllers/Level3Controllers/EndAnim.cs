using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ApplicationConstants;

public class EndAnim : MonoBehaviour {

    public FadeInOut endAnim;

    public void End () {
        StartCoroutine (Coroutine());
    }

        IEnumerator Coroutine()
    {
        print ("END ANIM: fade out started");
        endAnim.FadeOut(1);
        print ("END ANIM: fade out complete");
        yield return new WaitUntil(() => endAnim.GetImage().color.a == 1);
        endAnim.ShowText("LEVEL COMPLETED");
        yield return new WaitForSeconds(2);
        FindObjectOfType<SceneLoader>().LoadScene(SceneNames.menu);
    }
}
