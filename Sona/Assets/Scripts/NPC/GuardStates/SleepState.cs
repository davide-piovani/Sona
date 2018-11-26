using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SleepState : GuardState {

    private float radius = 0f;

    public override float GetRadius()
    {
        return radius;
    }

}
