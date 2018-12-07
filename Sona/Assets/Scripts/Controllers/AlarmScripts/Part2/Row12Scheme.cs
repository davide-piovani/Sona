using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row12Scheme : MonoBehaviour, RowInterfaceP2
{

    Transform[] _squares;
    public Material _allarmed;
    public Material _safe;

    void Start()
    {
        _squares = GetComponentsInChildren<Transform>();

    }

    public void Shot(int i)
    {

        if (i == 0)
        {
            //S
            _squares[1].gameObject.GetComponent<Renderer>().material = _allarmed;
            _squares[2].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[3].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[4].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[5].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[6].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[7].gameObject.GetComponent<Renderer>().material = _allarmed;
        }
        else if (i == 1)
        {
            //I
            _squares[1].gameObject.GetComponent<Renderer>().material = _allarmed;
            _squares[2].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[3].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[4].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[5].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[6].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[7].gameObject.GetComponent<Renderer>().material = _allarmed;
        }
        else if (i == 2)
        {
            //M
            _squares[1].gameObject.GetComponent<Renderer>().material = _allarmed;
            _squares[2].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[3].gameObject.GetComponent<Renderer>().material = _allarmed;
            _squares[4].gameObject.GetComponent<Renderer>().material = _allarmed;
            _squares[5].gameObject.GetComponent<Renderer>().material = _allarmed;
            _squares[6].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[7].gameObject.GetComponent<Renderer>().material = _allarmed;
        }
        else if (i == 3)
        {
            //M
            _squares[1].gameObject.GetComponent<Renderer>().material = _allarmed;
            _squares[2].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[3].gameObject.GetComponent<Renderer>().material = _allarmed;
            _squares[4].gameObject.GetComponent<Renderer>().material = _allarmed;
            _squares[5].gameObject.GetComponent<Renderer>().material = _allarmed;
            _squares[6].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[7].gameObject.GetComponent<Renderer>().material = _allarmed;
        }
        else if (i == 4)
        {
            //E
            _squares[1].gameObject.GetComponent<Renderer>().material = _allarmed;
            _squares[2].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[3].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[4].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[5].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[6].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[7].gameObject.GetComponent<Renderer>().material = _allarmed;
        }
        else if (i == 5)
        {
            //T
            _squares[1].gameObject.GetComponent<Renderer>().material = _allarmed;
            _squares[2].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[3].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[4].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[5].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[6].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[7].gameObject.GetComponent<Renderer>().material = _allarmed;
        }
        else if (i == 6)
        {
            //R
            _squares[1].gameObject.GetComponent<Renderer>().material = _allarmed;
            _squares[2].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[3].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[4].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[5].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[6].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[7].gameObject.GetComponent<Renderer>().material = _allarmed;
        }
        else if (i == 7)
        {
            //I
            _squares[1].gameObject.GetComponent<Renderer>().material = _allarmed;
            _squares[2].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[3].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[4].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[5].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[6].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[7].gameObject.GetComponent<Renderer>().material = _allarmed;
        }
        else if (i == 8)
        {
            //C
            _squares[1].gameObject.GetComponent<Renderer>().material = _allarmed;
            _squares[2].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[3].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[4].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[5].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[6].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[7].gameObject.GetComponent<Renderer>().material = _allarmed;
        }

    }

    public void Active()
    {
        _squares[1].gameObject.GetComponent<Renderer>().material = _allarmed;
        _squares[2].gameObject.GetComponent<Renderer>().material = _allarmed;
        _squares[3].gameObject.GetComponent<Renderer>().material = _allarmed;
        _squares[4].gameObject.GetComponent<Renderer>().material = _allarmed;
        _squares[5].gameObject.GetComponent<Renderer>().material = _allarmed;
        _squares[6].gameObject.GetComponent<Renderer>().material = _allarmed;
        _squares[7].gameObject.GetComponent<Renderer>().material = _allarmed;
    }
}
