using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrthoDiagMove : Move {
    public Direction direction;

    public OrthoDiagMove(Piece movingPiece, Space afterSpace, Direction direction) : base(movingPiece, afterSpace) {
        this.direction = direction;
    }
}
