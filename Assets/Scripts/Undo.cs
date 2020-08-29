using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Undo : MonoBehaviour {
    public static Move lastMove;

    void Update() {
        if (Input.GetKeyDown("u") && lastMove != null) {
            lastMove.undoMove();
            GameEvents.changeTurn.Invoke();
            GameObject.Find("chess manager").GetComponent<ChessDisplayManager>().updateBoardDisplay();
            lastMove = null;
        }
    }
}
