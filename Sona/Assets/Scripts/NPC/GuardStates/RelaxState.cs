using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class RelaxState : GuardState {

    private float radius = 5f;

    public override float GetRadius()
    {
        return radius;
    }

}
