using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagonalPawnMove : PawnMove {
    public Direction direction;

    public DiagonalPawnMove(Pawn movingPiece, Space newSpace, Direction direction) : base(movingPiece, newSpace) {
        this.direction = direction;
    }
}
