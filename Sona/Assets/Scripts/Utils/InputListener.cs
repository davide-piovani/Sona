using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputListener : MonoBehaviour {

    private bool checkForInput;
    protected InputListener oldListener;

    public bool IsInputActive() { return checkForInput; }

    public void ActiveInput() { checkForInput = true; }

    public void DisableInput() { checkForInput = false; }

    public void SetAsUniqueInputListener(InputListener oldInputListener){
        oldListener = oldInputListener;
        oldListener.DisableInput();
        ActiveInput();
    }

    protected void RestoreOldListener() {
        DisableInput();
        oldListener.ActiveInput();
    }
}
