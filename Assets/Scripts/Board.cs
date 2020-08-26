using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
    public static Space[,] board = new Space[8, 8];
    public static List<Piece> aliveWhitePieces;
    public static List<Piece> aliveBlackPieces;
    public static Colour turn;

    private void Awake() {
        GameEvents.changeTurn.AddListener(changeTurn);

        // Create the board.
        for (int file = 0; file < 8; file++) {
            for (int rank = 0; rank < 8; rank++) {
                board[file, rank] = new Space(file, rank);
            }
        }

        // Populate the board.
        // Pawns
        for (int file = 0; file < 8; file++) {
            board[file, 1].setPiece(new Pawn(board[file, 1], Colour.WHITE));

            board[file, 6].setPiece(new Pawn(board[file, 6], Colour.BLACK));
        }

        // Rooks
        board[0, 0].setPiece(new Rook(board[0, 0], Colour.WHITE));
        board[7, 0].setPiece(new Rook(board[7, 0], Colour.WHITE));
        board[0, 7].setPiece(new Rook(board[0, 7], Colour.BLACK));
        board[7, 7].setPiece(new Rook(board[7, 7], Colour.BLACK));

        // Bishops
        board[2, 0].setPiece(new Bishop(board[2, 0], Colour.WHITE));
        board[5, 0].setPiece(new Bishop(board[5, 0], Colour.WHITE));
        board[2, 7].setPiece(new Bishop(board[2, 7], Colour.BLACK));
        board[5, 7].setPiece(new Bishop(board[5, 7], Colour.BLACK));

        // Queens
        board[3, 0].setPiece(new Queen(board[3, 0], Colour.WHITE));
        board[3, 7].setPiece(new Queen(board[3, 7], Colour.BLACK));




        aliveWhitePieces = new List<Piece>();
        for (int file = 0; file < 8; file++) {
            for (int rank = 0; rank < 2; rank++) {
                if (!board[file, rank].isEmpty) {  // Won't need this check later.
                    aliveWhitePieces.Add(board[file, rank].piece);
                }
            }
        }

        aliveBlackPieces = new List<Piece>();
        for (int file = 0; file < 8; file++) {
            for (int rank = 6; rank < 8; rank++) {
                if (!board[file, rank].isEmpty) {  // Won't need this check later.
                    aliveBlackPieces.Add(board[file, rank].piece);
                }
            }
        }

        GameEvents.getReachableSpaces.Invoke();
    }

    public static void changeTurn() {
        if (turn == Colour.WHITE) {
            turn = Colour.BLACK;
        }
        else {
            turn = Colour.WHITE;
        }

        GameEvents.getReachableSpaces.Invoke();
    }
}

public enum Colour {
    WHITE,
    BLACK
};