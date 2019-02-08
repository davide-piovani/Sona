using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SettingsElement : MonoBehaviour {

    protected string elemenType;

    public string GetElementType() { return elemenType; }
    public abstract void Select(bool selected);

}
