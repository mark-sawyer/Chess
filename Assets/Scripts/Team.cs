using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team {
    public Space[,] board;
    public List<Piece> alivePieces;

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
            board[file, pawnRank].setPiece(new Pawn(board[file, pawnRank], colour));
            alivePieces.Add(board[file, pawnRank].piece);
        }

        // Rooks
        board[0, otherPieceRank].setPiece(new Rook(board[0, otherPieceRank], colour));
        board[7, otherPieceRank].setPiece(new Rook(board[7, otherPieceRank], colour));

        // Knights
        board[1, otherPieceRank].setPiece(new Knight(board[1, otherPieceRank], colour));
        board[6, otherPieceRank].setPiece(new Knight(board[6, otherPieceRank], colour));

        // Bishops
        board[2, otherPieceRank].setPiece(new Bishop(board[2, otherPieceRank], colour));
        board[5, otherPieceRank].setPiece(new Bishop(board[5, otherPieceRank], colour));

        // Queen
        board[3, otherPieceRank].setPiece(new Queen(board[3, otherPieceRank], colour));

        // King
        board[4, otherPieceRank].setPiece(new King(board[4, otherPieceRank], colour));

        for (int file = 0; file < 8; file++) {
            alivePieces.Add(board[file, pawnRank].piece);
            alivePieces.Add(board[file, otherPieceRank].piece);
        }
    }
}
