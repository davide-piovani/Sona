using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square3Controller : MonoBehaviour, SquareInterface
{

    Transform[] _squares;
    public Material _allarmed;
    public Material _safe;

    void Awake()
    {
        _squares = GetComponentsInChildren<Transform>();

        ClearSquares();

    }

    void ClearSquares()
    {
        for (int j = 1; j < 50; j++) {
            _squares[j].gameObject.GetComponent<Renderer>().material = _allarmed;
        }
    }

    public void Shot(int i)
    {

        ClearSquares();


        if (i == 0)
        {
            //0
            _squares[16].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[17].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[18].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[19].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[20].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[23].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[27].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[30].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[31].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[32].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[33].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[34].gameObject.GetComponent<Renderer>().material = _safe;
        }
        else if (i == 5)
        {
            //1
            _squares[23].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[24].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[25].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[26].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[27].gameObject.GetComponent<Renderer>().material = _safe;
        }
        else if (i == 1)
        {
            //2
            _squares[16].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[18].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[19].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[20].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[23].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[25].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[27].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[30].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[31].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[32].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[34].gameObject.GetComponent<Renderer>().material = _safe;
        }
        else if (i == 6)
        {
            //3
            _squares[16].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[18].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[20].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[23].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[25].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[27].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[30].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[31].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[32].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[33].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[34].gameObject.GetComponent<Renderer>().material = _safe;
        }
        else if (i == 2)
        {
            //4
            _squares[16].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[17].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[18].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[25].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[30].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[31].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[32].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[33].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[34].gameObject.GetComponent<Renderer>().material = _safe;
        }
        else if (i == 7)
        {
            //5
            _squares[16].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[17].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[18].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[20].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[23].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[25].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[27].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[30].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[32].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[33].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[34].gameObject.GetComponent<Renderer>().material = _safe;
        }
        else if (i == 3)
        {
            //6
            _squares[16].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[17].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[18].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[19].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[20].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[23].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[25].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[27].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[30].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[32].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[33].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[34].gameObject.GetComponent<Renderer>().material = _safe;
        }
        else if (i == 8)
        {
            //7
            _squares[16].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[23].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[30].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[31].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[32].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[33].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[34].gameObject.GetComponent<Renderer>().material = _safe;
        }
        else if (i == 4)
        {
            //8
            _squares[16].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[17].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[18].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[19].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[20].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[23].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[25].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[27].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[30].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[31].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[32].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[33].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[34].gameObject.GetComponent<Renderer>().material = _safe;
        }
        /*else if (i == 8)
        {
            //9
            _squares[16].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[17].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[18].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[20].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[23].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[25].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[27].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[30].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[31].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[32].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[33].gameObject.GetComponent<Renderer>().material = _safe;
            _squares[34].gameObject.GetComponent<Renderer>().material = _safe;
        }*/

    }

    /*public void Active()
    {
        _squares[1].gameObject.GetComponent<Renderer>().material = _allarmed;
        _squares[2].gameObject.GetComponent<Renderer>().material = _allarmed;
        _squares[3].gameObject.GetComponent<Renderer>().material = _allarmed;
        _squares[4].gameObject.GetComponent<Renderer>().material = _allarmed;
        _squares[5].gameObject.GetComponent<Renderer>().material = _allarmed;
        _squares[6].gameObject.GetComponent<Renderer>().material = _allarmed;
        _squares[7].gameObject.GetComponent<Renderer>().material = _allarmed;
    }*/

}