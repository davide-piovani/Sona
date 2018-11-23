using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ApplicationConstants;
using UnityStandardAssets.CrossPlatformInput;

public class MenuController : MonoBehaviour {

    [SerializeField] TextMeshProUGUI[] buttons;

    int currentButton = 0;
    int currentMenu = 0;
    bool buttonChanged = false;
    SceneLoader sceneLoader;

    private void Start(){
        sceneLoader = FindObjectOfType<SceneLoader>();
        LoadMenu(0);
    }

    // Update is called once per frame
    void Update () {
        checkButtonChanged();
        checkEnterButton();
    }

    private void LoadMenu(int menuIndex){
        bool[] activeButtons = MenuConstants.menuActiveButtons[menuIndex];
        string[] buttonText = MenuConstants.menuButtonsText[menuIndex];

        for (int i = 0; i < activeButtons.Length; i++){
            buttons[i].enabled = activeButtons[i];
            buttons[i].text = buttonText[i];
        }
        SelectButton(0);
        currentMenu = menuIndex;
    }

    private void checkButtonChanged(){
        float vertical = CrossPlatformInputManager.GetAxis("Vertical");

        if (!buttonChanged){
            if (vertical > 0){
                PreviousButton();
            } else if (vertical < 0) {
                NextButton();
            }
        } else {
            if (Mathf.Abs(vertical) <= Mathf.Epsilon) buttonChanged = false;
        }
    }

    private void checkEnterButton(){
        if (CrossPlatformInputManager.GetButtonDown(GameConstants.enterButton)){
            switch (currentMenu){
                case 0:
                    Menu0EnterPressed();
                    break;
                case 1:
                    Menu1EnterPressed();
                    break;
            }
        }
    }

    private void Menu0EnterPressed(){
        switch (currentButton){
            case 0:
                NewGame();
                break;
            case 1:
                LoadGame();
                break;
            case 2:
                SelectLevel();
                break;
            case 3:
                Settings();
                break;
            case 4:
                Prototypes();
                break;
        }
    }

    private void Menu1EnterPressed(){
        switch (currentButton){
            case 0:
                sceneLoader.LoadScene(SceneNames.prototype1);
                break;
            case 1:
                sceneLoader.LoadScene(SceneNames.prototype2);
                break;
            case 2:
                sceneLoader.LoadScene(SceneNames.prototype3);
                break;
            case 4:
                LoadMenu(0);
                break;
        }
    }

    private void PreviousButton(){
        int i = currentButton;

        if (i != 0) {
            while (i != 0) { if (SelectButton(--i)) return; }
        }
        i = buttons.Length - 1;

        while (!SelectButton(i)) i--;
    }

    private void NextButton(){
        int i = currentButton;

        if (i != buttons.Length-1){
            while (i != buttons.Length - 1) { if (SelectButton(++i)) return; }
        }
        i = 0;

        while (!SelectButton(i)) i++;
    }

    private bool SelectButton(int index){
        if (index >= 0 && index < buttons.Length){
            if (!buttons[index].enabled) return false;
            buttons[currentButton].color = MenuConstants.unselectedButtonColor;
            buttons[index].color = MenuConstants.selectedButtonColor;
            currentButton = index;
            buttonChanged = true;
            return true;
        }
        return false;
    }

    private void NewGame(){
        print("NewGame");
    }

    private void LoadGame(){
        print("LoadGame");
    }

    private void SelectLevel(){
        print("SelectLevel");
    }

    private void Settings(){
        print("Settings");
    }

    private void Prototypes(){
        LoadMenu(1);
    }
}
