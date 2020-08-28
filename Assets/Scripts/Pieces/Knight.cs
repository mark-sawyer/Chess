using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece {
    public Knight(Space space, Colour colour) : base(space, colour) {
        GameEvents.getReachableOrAttackingSpaces.AddListener(getReachableSpaces);
        value = 3;

        if (colour == Colour.BLACK) {
            value *= -1;
        }
    }

    public override void getReachableSpaces() {
        if (space != null) {
            reachableSpaces.Clear();

            int file = space.file;
            int rank = space.rank;

            checkToAddSpaceObserved(file + 2, rank + 1);
            checkToAddSpaceObserved(file + 1, rank + 2);

            checkToAddSpaceObserved(file - 2, rank + 1);
            checkToAddSpaceObserved(file - 1, rank + 2);

            checkToAddSpaceObserved(file + 2, rank - 1);
            checkToAddSpaceObserved(file + 1, rank - 2);

            checkToAddSpaceObserved(file - 2, rank - 1);
            checkToAddSpaceObserved(file - 1, rank - 2);
        }
    }

    public void checkToAddSpaceObserved(int checkedFile, int checkedRank) {
        if (inBoardRange(checkedFile) && inBoardRange(checkedRank)) {
            Space spaceObserved = board[checkedFile, checkedRank];
            spaceObserved.setBeingAttacked(colour);

            if (spaceObserved.isEmpty || spaceObserved.piece.colour != colour) {
                reachableSpaces.Add(spaceObserved);
            }
        }
    }

    public override GameObject getGameObject() {
        if (colour == Colour.WHITE) {
            return Resources.Load<GameObject>("White/white knight");
        }
        else {
            return Resources.Load<GameObject>("Black/black knight");
        }
    }
}
