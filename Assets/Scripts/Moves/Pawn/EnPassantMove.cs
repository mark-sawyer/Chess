using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnPassantMove : DiagonalPawnMove {
    public EnPassantMove(Piece movingPiece, Space afterSpace, Direction direction) : base(movingPiece, afterSpace, direction) { }
}
