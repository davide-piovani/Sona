using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputButtonManager : MonoBehaviour {

    public GameObject _kkk;

    // Use this for initialization
	void Start () {
        _kkk.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        _kkk.SetActive(true);
	}
}
