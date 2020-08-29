using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin {
    public Direction pinType;
    public int turnPinned;

    public Pin(Direction pinType, int turnPinned) {
        this.pinType = pinType;
        this.turnPinned = turnPinned;
    }
}