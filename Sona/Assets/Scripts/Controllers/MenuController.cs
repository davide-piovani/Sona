using UnityEngine;
using TMPro;
using ApplicationConstants;
using UnityStandardAssets.CrossPlatformInput;
using System;

public class MenuController : InputListener {

    [SerializeField] TextMeshProUGUI[] buttons;
    [SerializeField] GameObject alertObject;
    [SerializeField] AudioClip backgroundMusic;

    int currentButton = 0;
    int currentMenu = 0;
    bool buttonChanged = false;
    SceneLoader sceneLoader;
    Canvas canvas;

    GameSlot currentSlot;

    private void Start(){
        sceneLoader = FindObjectOfType<SceneLoader>();
        canvas = FindObjectOfType<Canvas>();
        LoadMenu(MenuType.mainMenu);
        PlayMenuMusic();
    }

    // Update is called once per frame
    void Update () {
        if (checkForInput) {
            checkButtonChanged();
            checkEnterButton();
        }
    }

    private void LoadMenu(MenuType menuType){
        int menuIndex = (int)menuType;
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
            if (vertical > 0) PreviousButton();
            if (vertical < 0) NextButton();
        } else {
            if (Mathf.Abs(vertical) <= Mathf.Epsilon) buttonChanged = false;
        }
    }

    private void checkEnterButton(){
        if (CrossPlatformInputManager.GetButtonDown(PlayersConstants.enterButton)){
            switch (currentMenu){
                case 0:
                    MainMenuController();
                    break;
                case 1:
                    NewGameMenuController();
                    break;
                case 2:
                    LoadGameMenuController();
                    break;
                case 3:
                    SelectLevelMenuController();
                    break;
            }
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

    private void PlayMenuMusic(){
        AudioController controller = FindObjectOfType<AudioController>();
        if (controller) controller.PlayBackgroundMusic(backgroundMusic);
    }


    //---------------------------------   MENU CONTROLLERS   ---------------------------------

    private void MainMenuController(){
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
        }
    }

    private void NewGameMenuController(){
        switch (currentButton){
            case 0:
            case 1:
            case 2:
                CreateNewSlot(currentButton);
                break;
            case 4:
                LoadMenu(MenuType.mainMenu);
                break;
        }
    }

    private void LoadGameMenuController(){
        switch (currentButton){
            case 0:
            case 1:
            case 2:
                LoadSlot(currentButton);
                break;
            case 4:
                LoadMenu(MenuType.mainMenu);
                break;
        }
    }

    private void SelectLevelMenuController(){
        switch (currentButton){
            case 0:
            case 1:
            case 2:
                sceneLoader.LoadScene(currentButton + 1);
                break;
            case 4:
                LoadMenu(MenuType.mainMenu);
                break;
        }
    }


    //---------------------------------   ACTIONS   ---------------------------------

    private void NewGame(){
        LoadMenu(MenuType.newGame);
        LoadSlots();
    }

    private void LoadGame(){
        LoadMenu(MenuType.loadGame);
        LoadSlots();
    }

    private void SelectLevel(){
        LoadMenu(MenuType.selectLevel);
    }

    private void Settings(){
        print("Settings");
    }

    private void CreateNewSlot(int slotNumber) {
        string slotName;
        GameSlot slot = SaveSystem.LoadGameSlot(slotNumber);

        if (slot != null) slotName = slot.name;
        else slotName = GameConstants.voidSlotName;

        currentSlot = new GameSlot(slotNumber);

        PresentAllert(GameConstants.createNewSlotAlertMessage, slotName, GameConstants.cancelString, 
                        GameConstants.okString, CreateSlot);
    }

    private void LoadSlot(int slotNumber) {
        string slotName;
        currentSlot = SaveSystem.LoadGameSlot(slotNumber);

        if (currentSlot != null) slotName = currentSlot.name;
        else return;

        PresentAllert(GameConstants.loadSlotAlertMessage, slotName, GameConstants.cancelString,
                        GameConstants.okString, CreateSlot);
    }

    private void LoadSlots() {
        GameSlot[] slots = SaveSystem.GetGameSlots();
        for(int i = 0; i < slots.Length; i++){
            if (slots[i] != null){
                buttons[i].text = slots[i].name;
            }
        }
    }

    private void PresentAllert(string message, string content, string cancel, string ok, Action<string> callback){
        GameObject alert = Instantiate(alertObject);
        AlertController alertController = alert.GetComponent<AlertController>();

        alertController.SetAllert(message, content, cancel, ok, callback);
        alertController.SetDimensions(canvas);
        alertController.SetInputListener(this);
    }



    //---------------------------------   CALLBACK   ---------------------------------

    public void CreateSlot(string useless){
        SaveSystem.SaveGameSlot(currentSlot, currentSlot.number);
        sceneLoader.LoadScene(SceneType.Level_1);
    }

    public void LoadSlot(string useless){
        sceneLoader.LoadScene(currentSlot.sceneNumber);
    }


    //---------------------------------   ENUMS   ---------------------------------
    private enum MenuType {
        mainMenu,
        newGame,
        loadGame,
        selectLevel
    }
}
