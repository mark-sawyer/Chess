using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnMove : Move {
    public PawnMove(Piece movingPiece, Space afterSpace) : base(movingPiece, afterSpace) { }

    public override void executeMove() {
        ((Pawn)movingPiece).hasMoved = true;

        base.executeMove();
    }
}
