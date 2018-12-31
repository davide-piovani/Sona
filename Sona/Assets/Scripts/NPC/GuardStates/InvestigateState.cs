using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestigateState : GuardState {

    /*
     * Il giocatore è entrato in un raggio per cui può essere visto da una guardia
     * Questo raggio è molto ampio. La guardia attiva quindi gli occhi ma continua a seguire il suo
     * percorso (che deve poter essere composto da zero, uno, due o più waypoint).
     * Gli occhi devono poter accorgersi quando un giocatore entra nell'angolo di visione della guardia
     * Deve quindi superare tre controlli:
     *      - Il giocatore deve essere dentro il raggio massimo di visione (se è in questo stato dovrebbe già esserlo)
     *      - L'angolo tra il giocatore e la guardia non deve essere superiore ad un certo valore scelto da noi
     *      - Non ci devono essere ostacoli tra il giocatore e la guardia, e il giocatore non deve essere invisibile
     *        (usare metodo IsVisible() di player)
     */

    private float radius = 7f;

    public override float GetRadius()
    {
        return radius;
    }

}
