using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnMove : Move {
    public bool promotion;
    public Piece pawnThatWasPromoted;

    public PawnMove(Piece movingPiece, Space newSpace) : base(movingPiece, newSpace) {
        this.movingPiece = movingPiece;
    }

    public override void executeMove() {
        if (Mathf.Abs(newSpace.rank - oldSpace.rank) == 2) {
            ((Pawn)movingPiece).turnMovedTwo = Board.turnNum;
        }
        else if (newSpace.rank % 7 == 0) {
            promotion = true;
            pawnThatWasPromoted = movingPiece;
            pawnThatWasPromoted.team.alivePieces.Remove(pawnThatWasPromoted);
            movingPiece = new Queen(movingPiece.colour);
            movingPiece.setTeam();
            movingPiece.team.alivePieces.Add(movingPiece);
        }
        Debug.Log("1: " + movingPiece);

        base.executeMove();
    }

    public override void undoMove() {
        if (Mathf.Abs(newSpace.rank - oldSpace.rank) == 2) {
            ((Pawn)movingPiece).turnMovedTwo = -999;
        }
        else if (promotion) {
            movingPiece.team.alivePieces.Remove(movingPiece);
            movingPiece.team.alivePieces.Add(pawnThatWasPromoted);
            movingPiece = pawnThatWasPromoted;
        }

        base.undoMove();
    }
}
