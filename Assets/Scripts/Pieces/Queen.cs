using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : OrthoDiagPiece {
    public Pawn host;

    public Queen(Colour colour) : base(colour) {
        GameEvents.getPlayableMoves.AddListener(getPlayableMoves);
        value = 9;
        isOrtho = true;
        isDiag = true;

        if (colour == Colour.BLACK) {
            value *= -1;
        }
    }

    public override GameObject getGameObject() {
        if (colour == Colour.WHITE) {
            return Resources.Load<GameObject>("White/white queen");
        }
        else {
            return Resources.Load<GameObject>("Black/black queen");
        }
    }

    public void removeListener() {
        GameEvents.getPlayableMoves.RemoveListener(getPlayableMoves);
    }
}
