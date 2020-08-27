using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : OrthoDiagPiece {
    public bool hasMoved;

    public Rook(Space space, Colour colour) : base(space, colour) {
        GameEvents.getReachableOrAttackingSpaces.AddListener(getReachableSpaces);
        value = 5;
        isOrtho = true;
        isDiag = false;

        if (colour == Colour.BLACK) {
            value *= -1;
        }
    }

    public override void setPosition(Space newSpace) {
        hasMoved = true;

        base.setPosition(newSpace);
    }

    public override GameObject getGameObject() {
        if (colour == Colour.WHITE) {
            return Resources.Load<GameObject>("White/white rook");
        }
        else {
            return Resources.Load<GameObject>("Black/black rook");
        }
    }
}
