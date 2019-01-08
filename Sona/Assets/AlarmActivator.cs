using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmActivator : Interactable {

    [SerializeField] GuardController[] guards;
    [SerializeField] float activeTime;

    private bool active = false;
    private float timePassed = 0f;

    public override void Interact(){
        ToggleAlarm(true);
    }

    private new void Update(){
        base.Update();
        if (active) {
            timePassed += TimeController.GetDelTaTime();
            if (timePassed > activeTime) ToggleAlarm(false);
        }
    }

    private void ToggleAlarm(bool on){
        active = on;
        gameController.ToggleSceneLights(!on);
        gameController.SetAlarm(on);
        gameController.ChangeAlarmState(on);
        foreach (GuardController guard in guards){
            guard.followingAlarm = on;
            if (on){
                guard.MoveTo(transform);
                guard.Run();
            }
        }
    }
}
