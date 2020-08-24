using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece {
    public bool hasMoved;
    public List<Space> reachableSpaces;
    public List<Piece> attackablePieces;

    public Pawn(Space space, Colour colour) : base(space, colour) { }

    public void getReachableSpaces() {
        int file = space.file;
        int rank = space.rank;

        // Black
        if (colour == Colour.BLACK && rank >= 1) {
            // Add space ahead if empty.
            if (board[file, rank - 1].isEmpty) {
                reachableSpaces.Add(board[file, rank - 1]);

                // Add two spaces ahead if empty and the pawn hasn't moved.
                if (!hasMoved && board[file, 4].isEmpty) {
                    reachableSpaces.Add(board[file, 4]);
                }
            }

            // Add diagonals.
            // Lower file.
            if (file >= 1 && !board[file - 1, rank -1].isEmpty && board[file - 1, rank - 1].piece.colour == Colour.WHITE) {
                reachableSpaces.Add(board[file - 1, rank - 1]);
                attackablePieces.Add(board[file - 1, rank - 1].piece);
            }
            // Higher file.
            if (file <= 6 && !board[file + 1, rank - 1].isEmpty && board[file + 1, rank - 1].piece.colour == Colour.WHITE) {
                reachableSpaces.Add(board[file + 1, rank - 1]);
                attackablePieces.Add(board[file + 1, rank - 1].piece);
            }

            // IMPLEMENT EN PASSANT LATER
        }

        // White
        else if (colour == Colour.WHITE && rank <= 6) {
            // Add space ahead if empty.
            if (board[file, rank + 1].isEmpty) {
                reachableSpaces.Add(board[file, rank + 1]);

                // Add two spaces ahead if empty and the pawn hasn't moved.
                if (!hasMoved && board[file, 3].isEmpty) {
                    reachableSpaces.Add(board[file, 3]);
                }
            }

            // Add diagonals.
            // Lower file.
            if (file >= 1 && !board[file - 1, rank + 1].isEmpty && board[file - 1, rank + 1].piece.colour == Colour.BLACK) {
                reachableSpaces.Add(board[file - 1, rank + 1]);
                attackablePieces.Add(board[file - 1, rank + 1].piece);
            }
            // Higher file.
            if (file <= 6 && !board[file + 1, rank + 1].isEmpty && board[file + 1, rank + 1].piece.colour == Colour.BLACK) {
                reachableSpaces.Add(board[file + 1, rank + 1]);
                attackablePieces.Add(board[file + 1, rank + 1].piece);
            }

            // IMPLEMENT EN PASSANT LATER
        }
    }

    public override void move() {

        reachableSpaces.Clear();
    }
}
