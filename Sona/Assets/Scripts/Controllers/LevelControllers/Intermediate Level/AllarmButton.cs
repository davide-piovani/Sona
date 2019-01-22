using ApplicationConstants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class AllarmButton : MonoBehaviour {

    [SerializeField] GuardController[] guards;
    [SerializeField] float activeTime;

    InteractController _interactController;
    GameController _gameController;
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
            if (timePassed > activeTime) {
                ToggleAlarm(false);
                timePassed = 0;
                _interactController.Necessary(true);
            }
        }
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
