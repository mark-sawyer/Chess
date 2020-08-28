using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : OrthoDiagPiece {
    public Bishop(Colour colour) : base(colour) {
        value = 3;
        isOrtho = false;
        isDiag = true;

        if (colour == Colour.BLACK) {
            value *= -1;
        }
    }

    public override GameObject getGameObject() {
        if (colour == Colour.WHITE) {
            return Resources.Load<GameObject>("White/white bishop");
        }
        else {
            return Resources.Load<GameObject>("Black/black bishop");
        }
    }
}
