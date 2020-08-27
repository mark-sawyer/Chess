﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEvents {
    public static UnityEvent removeReachableGameObjects = new UnityEvent();
    public static UnityEvent getReachableOrAttackingSpaces = new UnityEvent();
    public static UnityEvent getReachableSpacesKing = new UnityEvent();
    public static UnityEvent changeTurn = new UnityEvent();
    public static UnityEvent clearBeingAttacked = new UnityEvent();
}
