using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InsertString : MonoBehaviour {

    public Text _text;
    public Text/*MeshProUGUI*/ _pointer;
    float pointerSize;
    float initial_X_Pos;
    bool end = false;
    Canvas _canvas;
    
    void Awake () {
        _text.text = "";
        _pointer.text = "_";
        initial_X_Pos = _pointer.transform.localPosition.x;
        _canvas = FindObjectOfType<Canvas>();
        CalculatePointerSize();
        StartCoroutine("Pointer");
    }

    public void AddDigit(string digit) {
        _text.text += digit;
        _pointer.rectTransform.localPosition = new Vector3(PrintRightVertexPosX(_text.text.Length), _pointer.rectTransform.localPosition.y, _pointer.rectTransform.localPosition.z);
    }

    public void DeleteDigit() {
        if (_text.text.Equals("")) return;
        string newText = "";
        for (int i = 0; i < _text.text.Length - 1; i++) { newText += _text.text[i]; }
        _text.text = newText;
        _pointer.rectTransform.localPosition = new Vector3(PrintRightVertexPosX(_text.text.Length), _pointer.rectTransform.localPosition.y, _pointer.rectTransform.localPosition.z);
    }

    public string GetText() {
        end = true;
        return _text.text; }

    public int GetTextLenght() { return _text.text.Length; }

    IEnumerator Pointer() {
        while (!end) {
            yield return new WaitForSeconds(0.5f);
            _pointer.text = "_";
            yield return new WaitForSeconds(0.5f);
            _pointer.text = "";

        }
        _pointer.text = "";
        yield return null;
    }

    float PrintRightVertexPosX(int strLen)
    {
        if (_text.text.Length == 0) return initial_X_Pos;

        string text = _text.text;
        TextGenerator textGen = new TextGenerator(text.Length);
        Vector2 extents = _text.gameObject.GetComponent<RectTransform>().rect.size;
        textGen.Populate(text, _text.GetGenerationSettings(extents));
        int indexOfTextQuad = (strLen-1) * 4;

        float rightPosLastLetter = textGen.verts[indexOfTextQuad + 1].position.x;
        float leftPosLastLetter = textGen.verts[indexOfTextQuad].position.x;

        if(strLen == 10) return (rightPosLastLetter + leftPosLastLetter) / (2f * _canvas.scaleFactor);
        else return rightPosLastLetter/_canvas.scaleFactor + pointerSize/2f;
    }

    void CalculatePointerSize() {
        string text = _pointer.text;
        TextGenerator textGen = new TextGenerator(text.Length);
        Vector2 extents = _pointer.gameObject.GetComponent<RectTransform>().rect.size;
        textGen.Populate(text, _pointer.GetGenerationSettings(extents));

        pointerSize = (textGen.verts[1].position.x - textGen.verts[0].position.x) / _canvas.scaleFactor;
    }

}
