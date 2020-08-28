using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnPassantMove : DiagonalPawnMove {
    Space takenPieceSpace;

    public EnPassantMove(Pawn movingPiece, Space takenPawnSpace, Direction direction) : base(movingPiece, takenPawnSpace, direction) {
        takenPieceSpace = newSpace;
        this.newSpace = Board.board[newSpace.file, newSpace.rank + movingPiece.direction];
    }

    public override void executeMove() {
        takenPieceSpace.removePiece();
        takenPiece.team.alivePieces.Remove(takenPiece);
        
        oldSpace.removePiece();
        newSpace.setPiece(movingPiece);

        GameEvents.changeTurn.Invoke();
    }
}
