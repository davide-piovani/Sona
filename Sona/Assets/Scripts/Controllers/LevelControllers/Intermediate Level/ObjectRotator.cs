using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotator : MonoBehaviour {

    void Update()
    {
        transform.Rotate(new Vector3(10, 30, 45) * Time.deltaTime);
    }

}
