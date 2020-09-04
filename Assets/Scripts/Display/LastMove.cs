using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastMove : MonoBehaviour {
    void Start() {
        GameEvents.removeLastMove.AddListener(destroySelf);
        GameEvents.resetForNewGame.AddListener(destroySelf);
    }

    public void destroySelf() {
        Destroy(gameObject);
    }
}
