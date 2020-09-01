using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team {
    public Space[,] board;
    public List<Piece> allPieces;
    public List<Piece> alivePieces;
    public Piece king;
    public Colour colour;

    public Team(Colour colour) {
        this.colour = colour;
        board = Board.board;
        alivePieces = new List<Piece>();
        allPieces = new List<Piece>();

        int pawnRank;
        int otherPieceRank;
        if (colour == Colour.WHITE) {
            pawnRank = 1;
            otherPieceRank = 0;
        }
        else {
            pawnRank = 6;
            otherPieceRank = 7;
        }

        // Pawns
        for (int file = 0; file < 8; file++) {
            board[file, pawnRank].setPiece(new Pawn(colour));
        }

        // Rooks
        board[0, otherPieceRank].setPiece(new Rook(colour));
        board[7, otherPieceRank].setPiece(new Rook(colour));

        // Knights
        board[1, otherPieceRank].setPiece(new Knight(colour));
        board[6, otherPieceRank].setPiece(new Knight(colour));

        // Bishops
        board[2, otherPieceRank].setPiece(new Bishop(colour));
        board[5, otherPieceRank].setPiece(new Bishop(colour));

        // Queen
        board[3, otherPieceRank].setPiece(new Queen(colour));

        // King
        board[4, otherPieceRank].setPiece(new King(colour));
        king = board[4, otherPieceRank].piece;

        for (int file = 0; file < 8; file++) {
            alivePieces.Add(board[file, pawnRank].piece);
            alivePieces.Add(board[file, otherPieceRank].piece);
            allPieces.Add(board[file, pawnRank].piece);
            allPieces.Add(board[file, otherPieceRank].piece);

            board[file, pawnRank].piece.initialSpace = board[file, pawnRank].piece.space;
            board[file, otherPieceRank].piece.initialSpace = board[file, otherPieceRank].piece.space;
        }
    }

    public bool isCheckmated() {
        List<List<Move>> movesToRemoveLists = new List<List<Move>>();
        List<Move> movesToRemove;
        int totalPlayableMoves = 0;

        foreach (Piece piece in alivePieces) {
            // Create copy of playableMoves
            List<Move> playableMovesClone = new List<Move>();
            foreach (Move move in piece.playableMoves) {
                playableMovesClone.Add(move);
            }

            // Execute move. Check if the king is in check. Add to list if it is. Undo move.
            movesToRemove = new List<Move>();
            foreach (Move move in playableMovesClone) {
                move.executeMove();
                GameEvents.clearBeingAttacked.Invoke();
                GameEvents.getPlayableMoves.Invoke();
                GameEvents.filterPlayableMoves.Invoke();

                if (colour == Colour.WHITE) {
                    if (king.space.isBeingAttackedByBlack) {
                        movesToRemove.Add(move);
                    }
                }
                else {
                    if (king.space.isBeingAttackedByWhite) {
                        movesToRemove.Add(move);
                    }
                }

                move.undoMove();
                GameEvents.clearBeingAttacked.Invoke();
                GameEvents.getPlayableMoves.Invoke();
                GameEvents.filterPlayableMoves.Invoke();
            }

            // Add the moves to filter out to the list of lists.
            movesToRemoveLists.Add(movesToRemove);
        }

        // Match each move to remove to the new ones in playableMoves for each piece.
        // Place the matches in a new list, loop through it removing those items from playableMoves.
        for (int i = 0; i < movesToRemoveLists.Count; i++) {
            movesToRemove = new List<Move>();
            foreach (Move move in movesToRemoveLists[i]) {
                foreach (Move playableMove in alivePieces[i].playableMoves) {
                    if (move.isEqual(playableMove)) {
                        movesToRemove.Add(playableMove);
                        break;
                    }
                }
            }

            foreach (Move move in movesToRemove) {
                alivePieces[i].playableMoves.Remove(move);
            }

            totalPlayableMoves += alivePieces[i].playableMoves.Count;
        }

        return totalPlayableMoves == 0;
    }

    public bool isStalemated() {
        foreach (Piece piece in alivePieces) {
            if (piece.playableMoves.Count != 0) {
                return false;
            }
        }
        return true;
    }

    public void resetTeam() {
        alivePieces.Clear();

        foreach(Piece piece in allPieces) {
            alivePieces.Add(piece);

            piece.timesMoved = 0;
            if (piece.space != null) {
                piece.space.removePiece();
            }
            
            if (!piece.initialSpace.isEmpty) {
                piece.initialSpace.removePiece();
            }
            piece.initialSpace.setPiece(piece);
            piece.playableMoves.Clear();

            if (piece is Pawn) {
                ((Pawn)piece).isPromoted = false;
                ((Pawn)piece).promotionQueen.space = piece.initialSpace;
            }
        }
    }
}
