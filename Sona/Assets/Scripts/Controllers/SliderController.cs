using UnityEngine;
using TMPro;
using UnityEngine.UI;
using ApplicationConstants;

public class SliderController : SettingsElement {

    public SliderType type;
    [SerializeField] TextMeshProUGUI sliderText;
    [SerializeField] Slider slider;
    [SerializeField] Image sliderFillArea;

    public override void Select(bool selected) {
        if (selected) {
            sliderText.color = MenuConstants.selectedButtonColor;
            sliderFillArea.color = MenuConstants.sliderHighlightedColor;
        } else {
            sliderText.color = MenuConstants.unselectedButtonColor;
            sliderFillArea.color = MenuConstants.sliderNormalColor;
        }
    }

    public float GetSliderValue() {
        return slider.value;
    }

    public void ChangeSliderValue(float delta, bool increase) {
        float oldValue = slider.value;
        slider.value = (increase) ? (oldValue + delta) : (oldValue - delta);
    }

    public void SetValue(float value){
        slider.value = value;
    }
}
