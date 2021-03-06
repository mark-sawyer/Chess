﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnMove : Move {
    public bool promotion;

    public PawnMove(Piece movingPiece, Space newSpace) : base(movingPiece, newSpace) {
        this.movingPiece = movingPiece;
    }

    public override void executeMove() {
        if (Mathf.Abs(newSpace.rank - oldSpace.rank) == 2) {
            ((Pawn)movingPiece).turnMovedTwo = Board.turnNum;
        }
        else if (newSpace.rank % 7 == 0 && !((Pawn)movingPiece).isPromoted) {
            promotion = true;
            ((Pawn)movingPiece).isPromoted = true;
            ((Pawn)movingPiece).value *= 9;
        }

        base.executeMove();
    }

    public override bool executeRealMove() {
        executeMove();
        GameEvents.removeLastMove.Invoke();
        GameObject.Instantiate(Resources.Load<GameObject>("last move"), new Vector3(newSpace.file, newSpace.rank, 0), Quaternion.identity);
        GameObject.Instantiate(Resources.Load<GameObject>("last move"), new Vector3(oldSpace.file, oldSpace.rank, 0), Quaternion.identity);

        return true;
    }

    public override void undoMove() {
        if (Mathf.Abs(newSpace.rank - oldSpace.rank) == 2) {
            ((Pawn)movingPiece).turnMovedTwo = -999;
        }
        else if (promotion) {
            ((Pawn)movingPiece).isPromoted = false;
            ((Pawn)movingPiece).value /= 9;
        }

        base.undoMove();
    }
}
