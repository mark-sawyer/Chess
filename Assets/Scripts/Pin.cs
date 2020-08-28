using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin {
    public PinType pinType;

    public Pin(PinType pinType) {
        this.pinType = pinType;
    }
}

public enum PinType {
    HORIZONTAL,
    VERTICAL,
    POSITIVE,
    NEGATIVE
};
