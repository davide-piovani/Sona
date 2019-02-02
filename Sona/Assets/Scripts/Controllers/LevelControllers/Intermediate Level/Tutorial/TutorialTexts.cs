using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTexts : MonoBehaviour {

    private int i = 0;

    private string[] names = { "Hannah", "Charlie", "Charlie", "Hannah", "Hannah" };

    private string[] lines = {
        "Hey Jack! So you are not dead! We are Hannah and Charlie. We are stuck in this prison. Do you remember the explosion?",
        "Our beautiful city was destroyed and then the army come and take us. So many people die, how can we survived?",
        "We must exit from this room! The door key must be out of there. I know a way to take it!",
        "Well, good job Charlie! Now try to find the key and open this door!",
        "Ok, now I think it's better if I go exploring the rest of the area. So in case of danger I can exploit my skills"
    };


    public string GetName(int end)
    {
        if (i < end)
        {
            return (names[i]);
        }
        else
        {
            return null;
        }
    }

    public string GetDialContent(int end)
    {
        if (i < end)
        {
            string line = lines[i];
            i++;
            return (line);
        }
        else
        {
            return null;
        }
    }

}
