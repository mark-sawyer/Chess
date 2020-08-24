using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPiece : MonoBehaviour {
    void Start() {
        GameEvents.removePieces.AddListener(removePiece);
    }

    public void removePiece() {
        Destroy(gameObject);
    }
}
