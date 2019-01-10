using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerActiveBar : MonoBehaviour {

    public Color _startColor;
    public Color _activeColor;

    bool active = false;
    Image _img;

    void Start () {
		_img = GetComponent<Image>();
        _img.color = _startColor; 	
    }
	
	void Update () {
        if (active)
        {
            _img.color = _activeColor;
        }
        else {
            _img.color = _startColor;
        }
    }

    public void SetBarActive(bool cond) {
        active = cond;
    }
}
