using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Decision
{
    public abstract Action Decide(GuardController controller);
}
