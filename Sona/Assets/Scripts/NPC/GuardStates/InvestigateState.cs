using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestigateState : GuardState {

    private float radius = 7f;

    public override float GetRadius()
    {
        return radius;
    }

}
