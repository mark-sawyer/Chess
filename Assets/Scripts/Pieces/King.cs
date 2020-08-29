using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece {
    public King(Colour colour) : base(colour) { }

    public override void getPlayableMoves() {
        if (space != null) {
            playableMoves.Clear();
            int file = space.file;
            int rank = space.rank;

            setReaching(file, rank + 1);
            setReaching(file + 1, rank + 1);
            setReaching(file + 1, rank);
            setReaching(file + 1, rank - 1);
            setReaching(file, rank - 1);
            setReaching(file - 1, rank - 1);
            setReaching(file - 1, rank);
            setReaching(file - 1, rank + 1);
        }
    }

    public override GameObject getGameObject() {
        if (colour == Colour.WHITE) {
            return Resources.Load<GameObject>("White/white king");
        }
        else {
            return Resources.Load<GameObject>("Black/black king");
        }
    }

    private void setReaching(int goalFile, int goalRank) {
        if (inBoardRange(goalFile) && inBoardRange(goalRank)) {
            Space spaceObserved = board[goalFile, goalRank];
            if (spaceObserved.isEmpty || spaceObserved.piece.colour != colour) {
                spaceObserved.setBeingAttacked(colour);
                playableMoves.Add(new Move(this, spaceObserved));
            }
        }
    }

    public override void filterPlayableMoves() {
        List<Move> movesToRemove = new List<Move>();
        foreach (Move move in playableMoves) {
            if (colour == Colour.WHITE) {
                if (move.newSpace.isBeingAttackedByBlack) {
                    movesToRemove.Add(move);
                }
            }
            else {
                if (move.newSpace.isBeingAttackedByWhite) {
                    movesToRemove.Add(move);
                }
            }
        }

        foreach (Move move in movesToRemove) {
            playableMoves.Remove(move);
        }
    }
}
