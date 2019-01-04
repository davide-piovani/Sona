using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AllarmRow : MonoBehaviour {

    [SerializeField] public bool[] _shot0 = new bool[10];
    [SerializeField] public bool[] _shot1 = new bool[10];
    [SerializeField] public bool[] _shot2 = new bool[10];
    [SerializeField] public bool[] _shot3 = new bool[10];
    [SerializeField] public bool[] _shot4 = new bool[10];
    [SerializeField] public bool[] _shot5 = new bool[10];

    public bool[,] _rowScheme = new bool[6,10];
    public Material _allarmed;
    public Material _safe;


    AllarmedSquare[] _squares;
    
    void Start()
    {
        for (int j = 0; j < 10; j++) {
            _rowScheme[0, j] = _shot0[j];
        }
        for (int j = 0; j < 10; j++)
        {
            _rowScheme[1, j] = _shot1[j];
        }
        for (int j = 0; j < 10; j++)
        {
            _rowScheme[2, j] = _shot2[j];
        }
        for (int j = 0; j < 10; j++)
        {
            _rowScheme[3, j] = _shot3[j];
        }
        for (int j = 0; j < 10; j++)
        {
            _rowScheme[4, j] = _shot4[j];
        }
        for (int j = 0; j < 10; j++)
        {
            _rowScheme[5, j] = _shot5[j];
        }
        _squares = GetComponentsInChildren<AllarmedSquare>();

    }

    public void Shot(int i)
    {
        for (int j = 0; j < 10; j++) {
            if (_rowScheme[i, j])
            {
                _squares[j].Safe(true);
                _squares[j].gameObject.GetComponent<Renderer>().material = _safe;
            }
            else {
                _squares[j].Safe(false);
                _squares[j].gameObject.GetComponent<Renderer>().material = _allarmed;
            }
        }
        
    }
}
