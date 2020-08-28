using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team {
    public Space[,] board;
    public List<Piece> alivePieces;
    public Piece king;

    public Team(Colour colour) {
        board = Board.board;
        alivePieces = new List<Piece>();

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
            alivePieces.Add(board[file, pawnRank].piece);
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
        }
    }
}
