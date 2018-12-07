using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public ProgressBarCircle pb;
    public GameObject _environment;
    float value;
    bool active;
    bool consume;
    float rechargeSpeed = 6f;
    float consumeSpeed = 6f;

    void Start () {
        value = 100;
        active = false;
        consume = false;
        pb.BarValue = value;
    }
	
	void Update () {

        if (!active & !consume & Input.GetKeyDown(KeyCode.G)) {
            active = true;
            consume = true;
            TimeController.changeTime();
        }
        else if (active & consume & Input.GetKeyDown(KeyCode.G)){
            consume = false;
            TimeController.changeTime();
        }

        if (active & !consume & value<100) {
            value = value + rechargeSpeed * Time.deltaTime;
            if (value > 100) {
                value = 100;
            }
        }

        if (active & consume & value>0)
        {
            value = value - consumeSpeed * Time.deltaTime;
            if (value < 0){
                value = 0;
            }
        }

        if (active & !consume & value == 100) {
            active = false;
        }

        if (active & consume & value == 0){
            consume = false;
            TimeController.changeTime();
        }

        pb.BarValue = value;
    }
}
