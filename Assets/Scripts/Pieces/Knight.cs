using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece {
    public Knight(Colour colour) : base(colour) {
        value = 3;

        if (colour == Colour.BLACK) {
            value *= -1;
        }
    }

    public override void getPlayableMoves() {
        if (space != null) {
            playableMoves.Clear();

            int file = space.file;
            int rank = space.rank;

            if (pin == null) {
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
    }

    public void checkToAddSpaceObserved(int checkedFile, int checkedRank) {
        if (inBoardRange(checkedFile) && inBoardRange(checkedRank)) {
            Space spaceObserved = board[checkedFile, checkedRank];
            spaceObserved.setBeingAttacked(colour);

            if (spaceObserved.isEmpty || spaceObserved.piece.colour != colour) {
                playableMoves.Add(new Move(this, spaceObserved));
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

    public override void filterPlayableMoves() {
        if (pin != null) {
            playableMoves.Clear();            
            pin = null;            
        }
    }
}
