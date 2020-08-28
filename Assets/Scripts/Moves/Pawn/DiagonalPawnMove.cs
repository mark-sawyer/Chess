using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagonalPawnMove : PawnMove {
    public Direction direction;

    public DiagonalPawnMove(Piece movingPiece, Space afterSpace, Direction direction) : base(movingPiece, afterSpace) {
        this.direction = direction;
    }
}
