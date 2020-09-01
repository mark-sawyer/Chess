using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move {
    public Piece movingPiece;
    public Space newSpace;
    public Space oldSpace;
    public Piece takenPiece;

    public Move(Piece movingPiece, Space newSpace) {
        this.movingPiece = movingPiece;
        this.newSpace = newSpace;
        oldSpace = movingPiece.space;
        if (!newSpace.isEmpty) {
            takenPiece = newSpace.piece;
        }
    }

    public virtual void executeMove() {
        if (takenPiece is King) {
            Debug.Log(movingPiece);
        }

        if (!movingPiece.isHost) {
            movingPiece = ((Queen)movingPiece).host;
        }

        if (takenPiece != null) {
            newSpace.removePiece();
            takenPiece.team.alivePieces.Remove(takenPiece);
        }

        oldSpace.removePiece();
        newSpace.setPiece(movingPiece);

        movingPiece.timesMoved++;
        Board.turnNum++;
    }

    public virtual void undoMove() {
        if (!movingPiece.isHost) {
            movingPiece = ((Queen)movingPiece).host;
        }

        newSpace.removePiece();
        oldSpace.setPiece(movingPiece);

        if (takenPiece != null) {
            newSpace.setPiece(takenPiece);
            takenPiece.team.alivePieces.Add(takenPiece);
        }

        movingPiece.timesMoved--;
        Board.turnNum--;
        Board.gameIsOver = false;
        Board.gameIsStalemate = false;
    }

    public bool isEqual(Move comparisonMove) {
        return newSpace == comparisonMove.newSpace && oldSpace == comparisonMove.oldSpace;
    }
}
