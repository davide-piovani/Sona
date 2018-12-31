using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlatformMovement {

    void ActiveDeActivePlatform(bool cond);
    void MovePlatform();
    bool IsActive();
    bool IsMoving();
    bool IsEnd();
    bool IsStart();
    //bool ArePlayersOnPlatform();
    int PlayersOnPlatform();

}
