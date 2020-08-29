using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnMove : Move {
    new public Pawn movingPiece;

    public PawnMove(Pawn movingPiece, Space newSpace) : base(movingPiece, newSpace) {
        this.movingPiece = movingPiece;
    }

    public override void executeMove() {
        if (Mathf.Abs(newSpace.rank - oldSpace.rank) == 2) {
            movingPiece.turnMovedTwo = Board.turnNum;
        }

        base.executeMove();
    }

    public override void undoMove() {
        if (Mathf.Abs(newSpace.rank - oldSpace.rank) == 2) {
            movingPiece.turnMovedTwo = -999;
        }

        base.undoMove();
    }
}
