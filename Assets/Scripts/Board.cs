using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
    public static GameObject pawn;

    public static Space[,] board = new Space[8, 8];
    public static List<Piece> takenBlackPieces;
    public static List<Piece> takenWhitePieces;

    private void Awake() {
        // Set the prefab references.
        pawn = Resources.Load<GameObject>("pawn");

        // Create the board.
        for (int file = 0; file < 8; file++) {
            for (int rank = 0; rank < 8; rank++) {
                board[file, rank] = new Space(new int[file, rank]);
            }
        }

        // Populate the board.
        for (int file = 0; file < 8; file++) {
            board[file, 0].setPiece(new Pawn(board[file, 0], Colour.WHITE));
            board[file, 1].setPiece(new Pawn(board[file, 0], Colour.WHITE));
            board[file, 6].setPiece(new Pawn(board[file, 0], Colour.WHITE));
            board[file, 7].setPiece(new Pawn(board[file, 0], Colour.WHITE));
        }
    }

    public static void changeBoardDisplay() {
        GameEvents.removePieces.Invoke();

        for (int file = 0; file < 8; file++) {
            for (int rank = 0; rank < 8; rank++) {
                if (!board[file, rank].isEmpty) {
                    Instantiate(pawn, new Vector3(file, rank, 0), Quaternion.identity);
                }
            }
        }
    }
}
