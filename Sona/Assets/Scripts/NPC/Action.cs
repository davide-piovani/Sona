using UnityEngine;

public abstract class Action 
{
    public float catchingRadius;

    public abstract void Act(GuardController controller);
}
