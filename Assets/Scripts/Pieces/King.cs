using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece {
    public King(Space space, Colour colour) : base(space, colour) {
        GameEvents.getReachableOrAttackingSpaces.AddListener(setAttackingSpaces);
        GameEvents.getReachableSpacesKing.AddListener(getReachableSpaces);
        value = 1000;

        if (colour == Colour.BLACK) {
            value *= -1;
        }
    }

    public override void getReachableSpaces() {
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

    public void setAttackingSpaces() {
        reachableSpaces.Clear();
        int file = space.file;
        int rank = space.rank;

        setAttacking(file, rank + 1);
        setAttacking(file + 1, rank + 1);
        setAttacking(file + 1, rank);
        setAttacking(file + 1, rank - 1);
        setAttacking(file, rank - 1);
        setAttacking(file - 1, rank - 1);
        setAttacking(file - 1, rank);
        setAttacking(file - 1, rank + 1);
    }

    public override GameObject getGameObject() {
        if (colour == Colour.WHITE) {
            return Resources.Load<GameObject>("White/white king");
        }
        else {
            return Resources.Load<GameObject>("Black/black king");
        }
    }

    private bool safeToGoOn(Space space) {
        if (colour == Colour.WHITE) {
            return !space.isBeingAttackedByBlack;
        }
        else {
            return !space.isBeingAttackedByWhite;
        }
    }

    private void setReaching(int goalFile, int goalRank) {
        if (inBoardRange(goalFile) && inBoardRange(goalRank)) {
            Space spaceObserved = board[goalFile, goalRank];
            if (safeToGoOn(spaceObserved) && (spaceObserved.isEmpty || spaceObserved.piece.colour != colour)) {
                reachableSpaces.Add(spaceObserved);
            }
        }
    }

    private void setAttacking(int goalFile, int goalRank) {
        if (inBoardRange(goalFile) && inBoardRange(goalRank)) {
            board[goalFile, goalRank].setBeingAttacked(colour);
        }
    }
}
