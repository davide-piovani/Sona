using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlatformMovement {

    void ActiveDeActivePlatform(bool cond);
    void MovePlatform();
    void CalculateOffsets();
    bool IsActive();
    bool IsMoving();
    bool IsEnd();
    bool IsStart();
    int PlayersOnPlatform();

}
