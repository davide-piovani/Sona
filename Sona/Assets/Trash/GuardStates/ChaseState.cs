using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ChaseState : GuardState {

    /*
     * La guardia insegue il giocatore più vicino che è nel suo raggio di visione, se non vede più nessun
     * giocatore raggiunge l'ultimo punto in cui ha visto il giocatore    
     */

    private float radius = 0f;

    public override float GetRadius()
    {
        return radius;
    }

}
