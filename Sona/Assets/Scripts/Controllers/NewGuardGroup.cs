using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGuardGroup : MonoBehaviour {

    NewGuardController[] guards;
    public List<Player> targets = new List<Player>();

    void Start(){
        guards = this.GetComponentsInChildren<NewGuardController>();
    }

    public List<Player> GetGroupTargets() { return targets; }

    public void AddPlayer(Player player){
        if (!targets.Contains(player)) targets.Add(player);
    }

    public void RemoveTarget(Player player){
        if (!IsThereAGuardWhoCanSeeHim(player)) targets.Remove(player);
    }

    private bool IsThereAGuardWhoCanSeeHim(Player player){
        foreach (NewGuardController guard in guards){
            foreach(Player playerInGuardSigth in guard.GetVisiblePlayers()){
                if (playerInGuardSigth == player) return true;
            }
        }
        return false;
    }
}
