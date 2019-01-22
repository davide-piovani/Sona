using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface LockedDoor {

    void KeyTaken(string name);
    bool isKeyOwned();
    bool isKeyUsed();
    string KeyOwner();

}
