using ApplicationConstants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : InputListener {

    public InsertNameController controller;
    [SerializeField] KeyboardRowController[] rows;
    bool rowChanged = false;
    int currentRow = 0;

    void Start () {
        rows[0].ActiveRow(0);
        for (int i = 1; i < rows.Length; i++) {
            rows[i].DeactiveRow();
        }
	}

    void Update() {
        if (IsInputActive()) { CheckVerticalInput(); }
    }

    public void EndStringInsert(string name) {
        for (int i = 0; i < rows.Length; i++) rows[i].DeactiveRow();
        controller.NameChosen(name);
    }

    private void CheckVerticalInput() {
        float vertical = GameSettings.GetMenuAxis(PlayersConstants.y_Axis);

        if (vertical > 0) { if (!rowChanged) ChangeRow(false); }
        else if (vertical < 0) { if (!rowChanged) ChangeRow(true); }
        else { rowChanged = false; }
    }

    private void ChangeRow(bool cond) {
        rowChanged = true;
        SelectRow(currentRow, false);
        SelectRow(cond ? NextRow() : PreviousRow(), true);
    }

    private void SelectRow(int rowIndex, bool cond) {
        if (cond)
        {
            int buttonIndex = rows[currentRow].GetCurrentButton();
            if ((currentRow == 2 | currentRow == 0) & rowIndex == 3 & buttonIndex == 9) buttonIndex = 8;
            if (currentRow == 3 & (rowIndex == 2 | rowIndex == 0 ) & buttonIndex == 8) buttonIndex = 9;
            currentRow = rowIndex;
            rows[rowIndex].ActiveRow(buttonIndex);
        }
        else { rows[rowIndex].DeactiveRow(); }
    }

    private int NextRow() {
        int i = currentRow;

        while (i != rows.Length - 1) { if (rows[++i].enabled) return i; }
        i = 0;

        while (!rows[i].enabled) i++;
        return i;
    }

    private int PreviousRow() {
        int i = currentRow;

        while (i != 0) { if (rows[--i].enabled) return i; }
        i = rows.Length - 1;

        while (!rows[i].enabled) i--;
        return i;
    }
}
