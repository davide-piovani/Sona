using UnityEngine;
using ApplicationConstants;
using UnityStandardAssets.CrossPlatformInput;
using TMPro;

public class SettingsController : InputListener {

    [SerializeField] GameObject[] buttons;
    private int selectedButton = 0;
    private bool buttonChanged = false;
    private bool horizontalChanged = false;

    private float newEffectsVolume;

    private AudioSource backgroundAudioSource;
    private GameSlot gameSlot;

    private void Start(){
        backgroundAudioSource = FindObjectOfType<BackgroundAudioController>().GetComponent<AudioSource>();
        gameSlot = FindObjectOfType<SceneLoader>().GetGameSlot();
        newEffectsVolume = gameSlot.effectsVolume;

        SelectButton(0, true);
        for (int i = 1; i < buttons.Length; i++) SelectButton(i, false);

        InitializeSliders();
    }

    private void InitializeSliders(){
        SliderController slider = buttons[0].GetComponent<SliderController>();
        slider.SetValue(gameSlot.musicVolume);

        slider = buttons[1].GetComponent<SliderController>();
        slider.SetValue(gameSlot.effectsVolume);
    }

    private void Update(){
        float vertical = CrossPlatformInputManager.GetAxis("Vertical");
        float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");

        CheckVerticalInput(vertical);
        CheckHorizontalInput(horizontal);
        CheckEnterButton();
    }

    private void CheckVerticalInput(float vertical) {
        if (vertical > 0) {
            if (!buttonChanged) ChangeSelectedButton(false);
        } else if (vertical < 0) {
            if (!buttonChanged) ChangeSelectedButton(true);
        } else {
            buttonChanged = false;
        }
    }

    private void CheckHorizontalInput(float horizontal) {
        SliderController slider = buttons[selectedButton].GetComponent<SliderController>();

        if (horizontal > 0) {
            if (slider) slider.ChangeSliderValue(MenuConstants.deltaSliderValue, true);
            LastButtonsHorizontalMovement();
        } else if (horizontal < 0) {
            if (slider) slider.ChangeSliderValue(MenuConstants.deltaSliderValue, false);
            LastButtonsHorizontalMovement();
        } else {
            horizontalChanged = false;
        }

        if (slider && slider.type == SliderType.Music) backgroundAudioSource.volume = slider.GetSliderValue();
        if (slider && slider.type == SliderType.Effects) newEffectsVolume = slider.GetSliderValue();
    }

    public void ChangeSelectedButton(bool nextButton){
        SelectButton(selectedButton, false);
        SelectButton(nextButton ? NextButton() : PreviousButton(), true);
        buttonChanged = true;
        horizontalChanged = true;
    }

    private int NextButton(){
        if (selectedButton < buttons.Length - 1) return selectedButton + 1;
        else return 0;
    }

    private int PreviousButton(){
        if (selectedButton > 0) return selectedButton - 1;
        else return buttons.Length - 1;
    }

    public void SelectButton(int buttonIndex, bool selected){
        GameObject obj = buttons[buttonIndex];
        SliderController slider = obj.GetComponent<SliderController>();
        if (slider) {
            slider.Select(selected);
        } else {
            TextMeshProUGUI button = obj.GetComponent<TextMeshProUGUI>();
            button.color = selected ? MenuConstants.selectedButtonColor : MenuConstants.unselectedButtonColor;
        }
        if (selected) selectedButton = buttonIndex;
    }

    private void LastButtonsHorizontalMovement() {
        if (!horizontalChanged){
            if (selectedButton == buttons.Length - 2) ChangeSelectedButton(true);
            else if (selectedButton == buttons.Length - 1) ChangeSelectedButton(false);
        }
    }

    private void CheckEnterButton(){
        if (CrossPlatformInputManager.GetButtonDown(PlayersConstants.enterButton)){
            AudioEffects.PlaySound(AudioEffects.instance.menuButtonClicked);
            switch (selectedButton){
                case 2:
                    ReturnBack();
                    break;
                case 3:
                    SaveData();
                    break;
            }
        }
    }

    private void Cancel(){
        RestoreOldValues();
        ReturnBack();
    }

    private void ReturnBack() {
        RestoreOldListener();
        RestoreOldMenu();
    }

    private void RestoreOldValues(){
        backgroundAudioSource.volume = gameSlot.musicVolume;
    }

    private void RestoreOldMenu(){
        gameObject.SetActive(false);

        Transform oldListenerTransform = oldListener.gameObject.transform;
        for (int i = 0; i < oldListenerTransform.childCount; i++) 
            oldListenerTransform.GetChild(i).gameObject.SetActive(true);
    }

    private void SaveData(){
        gameSlot.musicVolume = backgroundAudioSource.volume;
        gameSlot.effectsVolume = newEffectsVolume;
        gameSlot.Save();
        ReturnBack();
    }
}
