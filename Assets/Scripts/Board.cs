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
        aliveBlackPieces = new List<Piece>();
        aliveWhitePieces = new List<Piece>();
        for (int file = 0; file < 8; file++) {
            board[file, 1].setPiece(new Pawn(board[file, 1], Colour.WHITE));
            aliveWhitePieces.Add(board[file, 1].piece);

            board[file, 6].setPiece(new Pawn(board[file, 6], Colour.BLACK));
            aliveBlackPieces.Add(board[file, 6].piece);
        }

        board[0, 0].setPiece(new Rook(board[0, 0], Colour.WHITE));
        board[7, 0].setPiece(new Rook(board[7, 0], Colour.WHITE));
        board[0, 7].setPiece(new Rook(board[0, 7], Colour.BLACK));
        board[7, 7].setPiece(new Rook(board[7, 7], Colour.BLACK));

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