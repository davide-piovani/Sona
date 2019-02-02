using ApplicationConstants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class AllarmButton : MonoBehaviour {

    public Grate _door;
    [SerializeField] GuardController[] guards;
    [SerializeField] float activeTime;

    InteractController _interactController;
    GameController _gameController;
    bool guardPassed = false;
    bool active = false;
    float timePassed = 0f;

    void Start() {
        _interactController = GetComponent<InteractController>();
        _gameController = FindObjectOfType<GameController>();
    }

    void Update() {

        if (_interactController.IsActive() & CrossPlatformInputManager.GetButtonDown(PlayersConstants.interactButton)) {
            _interactController.Necessary(false);
            ToggleAlarm(true);
        }

        if (active)
        {
            timePassed += TimeController.GetDelTaTime();
            if (guardPassed) { _door.LockDoor(); }
            if (timePassed > activeTime) {
                ToggleAlarm(false);
                timePassed = 0;
                _interactController.Necessary(true);
            }
        }
    }

    public void GuardPassed() {
        guardPassed = true;
    }

    private void ToggleAlarm(bool on)
    {
        active = on;
        //_gameController.ToggleSceneLights(!on);
        _gameController.SetAlarm(on);
        _gameController.ChangeAlarmState(on);
        foreach (GuardController guard in guards)
        {
            guard.followingAlarm = on;
            if (on)
            {
                guard.MoveTo(transform);
                guard.Run();
            }
        }
    }
}
