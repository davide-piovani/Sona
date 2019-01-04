using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PatrolState : GuardState {

    /*
     * Non ci sono giocatori nel raggio della guardia, la guardia si muove seguendo il percorso preimpostato
     * composto da zero, uno, due o più waypoint    
     */

    private float radius = 5f;

    public override float GetRadius()
    {
        return radius;
    }

}
