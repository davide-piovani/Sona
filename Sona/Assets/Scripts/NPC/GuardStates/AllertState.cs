using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class AllertState : GuardState {

    /*
     * La guardia si blocca e si guarda attorno, entra in questo stato perchè il giocatore
     * si è avvicinato molto (raggio molto piccolo)    
    */

    private float radius = 8f;

    public override float GetRadius(){
       return radius;
    }

}
