using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEvents {
    public static UnityEvent removeReachableGameObjects = new UnityEvent();
    public static UnityEvent resetForNewGame = new UnityEvent();
    public static UnityEvent getPlayableMoves = new UnityEvent();
    public static UnityEvent filterPlayableMoves = new UnityEvent();
    public static UnityEvent changeTurn = new UnityEvent();
    public static UnityEvent clearBeingAttacked = new UnityEvent();
    public static UnityEvent setTeam = new UnityEvent();
    public static UnityEvent removeLastMove = new UnityEvent();
    public static BoolUnityEvent changeCompressability = new BoolUnityEvent();
}

public class BoolUnityEvent : UnityEvent<bool> { }