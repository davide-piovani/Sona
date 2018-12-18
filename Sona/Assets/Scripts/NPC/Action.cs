using UnityEngine;

public abstract class Action 
{
    public float catchingRadius;

    public abstract void Act(GuardController controller);
    public abstract bool ActionComplete(GuardController controller);
}
