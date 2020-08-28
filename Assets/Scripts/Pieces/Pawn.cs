using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece {
    private int direction;
    public bool hasMoved;

    public Pawn(Space space, Colour colour) : base(space, colour) {
        GameEvents.getPlayableMoves.AddListener(getPlayableMoves);
        value = 1;

        if (colour == Colour.WHITE) {
            direction = 1;
        }
        else {
            direction = -1;
            value *= -1;
        }
    }

    public override void getPlayableMoves() {
        if (space != null) {
            playableMoves.Clear();

            int file = space.file;
            int rank = space.rank;

            // Add space ahead if empty.
            if (rank % 7 >= 1 && board[file, rank + direction].isEmpty) {
                playableMoves.Add(new PawnMove(this, board[file, rank + direction]));

                // Add two spaces ahead if empty and the pawn hasn't moved.
                if (!hasMoved && board[file, rank + (2 * direction)].isEmpty) {
                    playableMoves.Add(new PawnMove(this, board[file, rank + (2 * direction)]));
                }
            }

            // Add diagonals.
            Space spaceObserved;
            // Lower file.
            if (rank % 7 >= 1 && file >= 1) {
                spaceObserved = board[file - 1, rank + direction];
                spaceObserved.setBeingAttacked(colour);
                if (!spaceObserved.isEmpty && spaceObserved.piece.colour != colour) {
                    playableMoves.Add(new PawnMove(this, spaceObserved));
                }
            }

            // Higher file.
            if (rank % 7 >= 1 && file <= 6) {
                spaceObserved = board[file + 1, rank + direction];
                spaceObserved.setBeingAttacked(colour);
                if (!spaceObserved.isEmpty && spaceObserved.piece.colour != colour) {
                    playableMoves.Add(new PawnMove(this, spaceObserved));
                }
            }
        }
    }

    public override GameObject getGameObject() {
        if (colour == Colour.WHITE) {
            return Resources.Load<GameObject>("White/white pawn");
        }
        else {
            return Resources.Load<GameObject>("Black/black pawn");
        }
    }

    public override void filterPlayableMoves() {

    }
}
