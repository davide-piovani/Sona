using UnityEngine;
using TMPro;
using ApplicationConstants;
using UnityStandardAssets.CrossPlatformInput;
using System;

public class MenuController : MenuMovementController {

    [SerializeField] GameObject alertObject;
    [SerializeField] GameObject insertNameAlert;
    [SerializeField] AudioClip backgroundMusic;
    [SerializeField] GameObject menu;
    [SerializeField] SettingsController settingsController;

    int currentMenu = 0;
    bool windowActive = false;
    SceneLoader sceneLoader;
    Canvas canvas;

    GameSlot currentSlot;

    private void Start(){
        GameSettings.SetControllerType(GameConstants.keyboardPad); //da mettere che riconosce se c'è joystick
        GameSettings.SetCurrentSceneNumber(1);
        sceneLoader = FindObjectOfType<SceneLoader>();
        canvas = FindObjectOfType<Canvas>();
        LoadMenu(MenuType.mainMenu);
        ActiveInput();
        InitializeMenu();
        PlayMenuMusic();
    }

    // Update is called once per frame
    void Update () {
        if (IsInputActive()) {
            CheckVerticalInput();
            CheckEnterButton();
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
        SelectButton(0, true);
        for (int i = 1; i < buttons.Length; i++) SelectButton(i, false);
        currentMenu = menuIndex;
    }

    private void CheckEnterButton(){
        if (GameSettings.GetButtonDown(PlayersConstants.enterButton)){
            //AudioEffects.PlaySound(AudioEffects.instance.menuButtonClicked);
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

    private void PlayMenuMusic(){
        BackgroundAudioController controller = FindObjectOfType<BackgroundAudioController>();
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
            case 4:
                sceneLoader.LoadScene(5);
                break;
        }
    }

    private void NewGameMenuController(){
        GameSettings.SetPlayMode(GameConstants.historyMode);
        switch (currentButton){
            case 0:
                CreateNewSlot(currentButton);
                break;
            case 1:
                CreateNewSlot(currentButton);
                break;
            case 2:
                CreateNewSlot(currentButton);
                break;
            case 4:
                LoadMenu(MenuType.mainMenu);
                break;
        }
    }

    private void LoadGameMenuController(){
        GameSettings.SetPlayMode(GameConstants.historyMode);
        switch (currentButton){
            case 0:
                LoadSlot(currentButton);
                break;
            case 1:
                LoadSlot(currentButton);
                break;
            case 2:
                LoadSlot(currentButton);
                break;
            case 4:
                LoadMenu(MenuType.mainMenu);
                break;
        }
    }

    private void SelectLevelMenuController(){
        GameSettings.SetPlayMode(GameConstants.levelMode);
        switch (currentButton){
            case 0:
                sceneLoader.LoadScene(2);
                break;
            case 1:
                sceneLoader.LoadScene(3);
                break;
            case 2:
                sceneLoader.LoadScene(5);
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
        ShowMenu(false);
        settingsController.gameObject.SetActive(true);
        settingsController.SetAsUniqueInputListener(this);
    }

    public void ShowMenu(bool visible){
        menu.SetActive(visible);
    }

    private void CreateNewSlot(int slotNumber) {
        string slotName;
        GameSlot slot = SaveSystem.LoadGameSlot(slotNumber);

        if (slot != null) slotName = slot.name;
        else slotName = GameConstants.voidSlotName;

        currentSlot = new GameSlot(slotNumber);

        if (slot != null) {
            PresentAllert(GameConstants.overwriteSlotAlertMessage, slotName, GameConstants.cancelString,
        GameConstants.okString, InsertName);
        }
        else {
            PresentAllert(GameConstants.createNewSlotAlertMessage, slotName, GameConstants.cancelString,
                        GameConstants.okString, InsertName);
        }
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

    private void PresentAllert(string message, string content, string cancel, string ok, Action<string> callback) {
        GameObject alert = Instantiate(alertObject);
        AlertController alertController = alert.GetComponent<AlertController>();
        alertController.SetAllert(message, content, cancel, ok, callback);
        alertController.SetDimensions(canvas);
        alertController.SetAsUniqueInputListener(this);
    }

    private void PresentInsertNameWindow(string cancel, string ok, Action<string> callback) {
        GameObject alert = Instantiate(insertNameAlert);
        InsertNameController alertController = alert.GetComponent<InsertNameController>();
        alertController.SetAllert(cancel, ok, callback);
        alertController.SetDimensions(canvas);
        alertController.SetAsUniqueInputListener(this);
    }



    //---------------------------------   CALLBACK   ---------------------------------

    public void InsertName(string useless) {
        PresentInsertNameWindow(GameConstants.cancelString, GameConstants.okString, CreateSlot);
    }

    public void CreateSlot(string newName){
        currentSlot.name = newName;
        SaveSystem.SaveGameSlot(currentSlot);
        sceneLoader.SetGameSlot(currentSlot);
        GameSettings.SetCurrentSceneNumber(2);
        sceneLoader.LoadScene(SceneType.Level_1);
    }

    public void LoadSlot(string useless){
        sceneLoader.SetGameSlot(currentSlot);
        GameSettings.SetCurrentSceneNumber(currentSlot.sceneNumber);
        sceneLoader.LoadScene(currentSlot.sceneNumber);
    }

    public void RestoreMainMenu() {
        ShowMenu(true);
        LoadMenu(MenuType.mainMenu);
    }

    //---------------------------------   ENUMS   ---------------------------------
    private enum MenuType {
        mainMenu,
        newGame,
        loadGame,
        selectLevel
    }
}