using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InsertCombination : MonoBehaviour {

    Text _combinationText;

    void Start () {
        _combinationText = GetComponentInChildren<Text>();
        _combinationText.text = "";
	}
	
	void Update () {

	}

    public void AddDigit(string str) {
        _combinationText.text += str;
    }

}
