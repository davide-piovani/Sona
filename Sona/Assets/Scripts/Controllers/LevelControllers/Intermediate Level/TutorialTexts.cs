using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTexts : MonoBehaviour {

    private int i = 0;
    private int j = 0;

    private string[] names = { "Hannah", "Charlie", "Charlie" };

    private string[] lines = {
        "Hey Jack! So you are not dead! We are Hannah and Charlie. We are stuck in this prison. Do you remember the explosion?",
        "Our beautiful city was destroyed and then the army come and take us. So many people die, how can we survived?",
        "We must exit from this room! The door key must be out of there. I know a way to take it!"
    };

    // press n to change character and z to use my power and dash through the door.

    public string GetName()
    {
        if (i < names.Length)
        {
            return (names[i]);
        }
        else
        {
            return null;
        }
    }

    public string GetDialContent()
    {
        if (i < lines.Length)
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
