using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHelper : MonoBehaviour {

    bool[] tutorialsComplete;

    private void Start()
    {
        for (int i=0; i< tutorialsComplete.Length; i++)
        {
            tutorialsComplete[i] = false;
        }
    }

    public void setTutorialSeen (int i)
    {
        tutorialsComplete[i] = true;
    }

    public bool checkIfTutorialCanBeDisplayed(int i)
    {
        return tutorialsComplete[i];
    }

}
