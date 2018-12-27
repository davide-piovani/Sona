using UnityEngine;
using TMPro;
using ApplicationConstants;
using UnityStandardAssets.CrossPlatformInput;
using System;

public class AlertController : MonoBehaviour {

    [SerializeField] TextMeshProUGUI messageLabel;
    [SerializeField] TextMeshProUGUI contentLabel;
    [SerializeField] TextMeshProUGUI cancelLabel;
    [SerializeField] TextMeshProUGUI okLabel;

    private int currentButton;
    private Action<string> callback;
    private bool buttonChanged = false;

    private InputListener inputListener;

    private void Start(){
        currentButton = 0;
        SelectButton(currentButton);
    }

    public void SetAllert(string message, string content, string cancelMessage, string okMessage, Action<string> callbackFunc){
        messageLabel.text = message;
        contentLabel.text = content;
        cancelLabel.text = cancelMessage;
        okLabel.text = okMessage;
        callback = callbackFunc;
        gameObject.SetActive(true);
    }

    public void SetDimensions(Canvas canvas){
        transform.SetParent(canvas.transform);
        RectTransform rectTransform = GetComponent<RectTransform>();

        rectTransform.anchoredPosition = new Vector2(0f, 0f);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, GameConstants.alertWidth);
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, GameConstants.alertHeight);
        rectTransform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void SetInputListener(InputListener listener){
        inputListener = listener;
        listener.checkForInput = false;
    }

    // Update is called once per frame
    void Update(){
        checkSelection();
        checkEnterButton();
    }

    private void SelectButton(int index){
        if (index == 0){
            cancelLabel.color = MenuConstants.selectedButtonColor;
            okLabel.color = MenuConstants.unselectedButtonColor;
            currentButton = index;
        }
        if (index == 1){
            cancelLabel.color = MenuConstants.unselectedButtonColor;
            okLabel.color = MenuConstants.selectedButtonColor;
            currentButton = index;
        }
    }

    private void checkSelection(){
        float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");

        if (horizontal > 0 || horizontal < 0){
            if (!buttonChanged) NextButton();
        } else {
            buttonChanged = false;
        }
    }

    private void NextButton(){
        buttonChanged = true;
        if (currentButton == 0) currentButton++;
        else currentButton = 0;

        SelectButton(currentButton);
    }

    private void checkEnterButton(){
        if (CrossPlatformInputManager.GetButtonDown(PlayersConstants.enterButton)){
            if (currentButton == 1) callback("boh");
            print(gameObject.name);
            inputListener.checkForInput = true;
            Destroy(gameObject);
        }
    }

}
