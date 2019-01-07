using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class DocManager : MonoBehaviour {

    public TextMeshProUGUI doc_title;
    public TextMeshProUGUI doc_content;

    public void ShowDoc(String title, String content){
        doc_content.text = content;
        doc_title.text = title;
    }

    public void Erase() {
        doc_title.text = "";
        doc_content.text = "";
    }
}
