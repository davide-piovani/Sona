using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This is an abstract class in State Pattern and represent all states which can be assumed by guards
 * 
 **/
public abstract class GuardState {

    //radius in which guard will detect player, is setted privately by each state with different radius
    public float catchingRadius { get; set; }

    //abstract methods
    abstract public float GetRadius();

}
