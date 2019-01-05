using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CharacterPortrait : MonoBehaviour {

    private GameController controller;
    private Image portrait;

    // Use this for initialization
    void Start ()
    {
        controller = FindObjectOfType<GameController>();
        portrait = gameObject.GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        portrait.sprite = controller.GetActivePlayer().GetCharacterPortrait();
    }
}
