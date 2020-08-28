using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnMove : Move {
    new public Pawn movingPiece;

    public PawnMove(Pawn movingPiece, Space newSpace) : base(movingPiece, newSpace) {
        this.movingPiece = movingPiece;
    }

    public override void executeMove() {
        movingPiece.hasMoved = true;
        if (Mathf.Abs(newSpace.rank - oldSpace.rank) == 2) {
            movingPiece.justMovedTwo = true;
        }

        base.executeMove();
    }

    public override void undoMove() {
        if (Mathf.Abs(newSpace.rank - oldSpace.rank) == 2) {
            movingPiece.justMovedTwo = false;
            movingPiece.hasMoved = false;
        }
        else if ((movingPiece.colour == Colour.WHITE && newSpace.rank == 2) || (movingPiece.colour == Colour.BLACK && newSpace.rank == 5)) {
            movingPiece.hasMoved = false;
        }

        base.undoMove();
    }
}
