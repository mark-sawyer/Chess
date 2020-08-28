﻿using System.Collections;
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
        if (takenPiece != null) {
            newSpace.removePiece();
            takenPiece.team.alivePieces.Remove(takenPiece);
        }

        oldSpace.removePiece();
        newSpace.setPiece(movingPiece);

        GameEvents.changeTurn.Invoke();
    }

    public virtual void undoMove() {
        newSpace.removePiece();
        oldSpace.setPiece(movingPiece);

        if (takenPiece != null) {
            newSpace.setPiece(takenPiece);
            takenPiece.team.alivePieces.Add(takenPiece);
        }

        GameEvents.changeTurn.Invoke();
    }
}