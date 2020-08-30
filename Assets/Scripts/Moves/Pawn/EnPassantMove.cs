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
        ((Pawn)movingPiece).promotionQueen.space = newSpace;

        takenPieceSpace.removePiece();
        takenPiece.team.alivePieces.Remove(takenPiece);

        oldSpace.removePiece();
        newSpace.setPiece(movingPiece);

        movingPiece.timesMoved++;
        Board.turnNum++;
    }

    public override void undoMove() {
        ((Pawn)movingPiece).promotionQueen.space = oldSpace;
        newSpace.removePiece();
        oldSpace.setPiece(movingPiece);
        takenPieceSpace.setPiece(takenPiece);
        takenPiece.team.alivePieces.Add(takenPiece);

        movingPiece.timesMoved--;
        Board.turnNum--;
        Board.gameIsOver = false;
    }
}
