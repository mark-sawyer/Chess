using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece {
    private int direction;
    public bool hasMoved;

    public Pawn(Space space, Colour colour) : base(space, colour) {
        value = 1;

        if (colour == Colour.WHITE) {
            direction = 1;
        }
        else {
            direction = -1;
            value *= -1;
        }
    }

    public override void getReachableSpaces() {
        reachableSpaces.Clear();

        int file = space.file;
        int rank = space.rank;

        // Add space ahead if empty.
        if (rank % 7 >= 1 && board[file, rank + direction].isEmpty) {
            reachableSpaces.Add(board[file, rank + direction]);

            // Add two spaces ahead if empty and the pawn hasn't moved.
            if (!hasMoved && board[file, rank + (2 * direction)].isEmpty) {
                reachableSpaces.Add(board[file, rank + (2 * direction)]);
            }
        }

        // Add diagonals.
        // Lower file.
        if (rank % 7 >= 1 && file >= 1 && !board[file - 1, rank + direction].isEmpty && board[file - 1, rank + direction].piece.colour != colour) {
            reachableSpaces.Add(board[file - 1, rank + direction]);
        }
        // Higher file.
        if (rank % 7 >= 1 && file <= 6 && !board[file + 1, rank + direction].isEmpty && board[file + 1, rank + direction].piece.colour != colour) {
            reachableSpaces.Add(board[file + 1, rank + direction]);
        }
    }

    public override void setPosition(Space newSpace) {
        hasMoved = true;

        base.setPosition(newSpace);
    }

    public override GameObject getGameObject() {
        if (colour == Colour.WHITE) {
            return Resources.Load<GameObject>("White/white pawn");
        }
        else {
            return Resources.Load<GameObject>("Black/black pawn");
        }
    }
}
