using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
    public static Space[,] board = new Space[8, 8];
    public static Colour turn;
    public static Team whiteTeam;
    public static Team blackTeam;
    public static int turnNum;

    private void Awake() {
        GameEvents.changeTurn.AddListener(changeTurn);

        // Create the board.
        for (int file = 0; file < 8; file++) {
            for (int rank = 0; rank < 8; rank++) {
                board[file, rank] = new Space(file, rank);
            }
        }

        // Populate the board.
        whiteTeam = new Team(Colour.WHITE);
        blackTeam = new Team(Colour.BLACK);
        GameEvents.setTeam.Invoke();
        GameEvents.getPlayableMoves.Invoke();
        turnNum = 1;
    }

    public static void changeTurn() {
        GameEvents.clearBeingAttacked.Invoke();
        GameEvents.getPlayableMoves.Invoke();
        GameEvents.filterPlayableMoves.Invoke();

        // Change turn and check if the king is in check.
        if (turn == Colour.WHITE) {
            turn = Colour.BLACK;
            if (blackTeam.king.space.isBeingAttackedByWhite) {
                blackTeam.filterToOutOfCheckMoves();
            }
        }
        else {
            turn = Colour.WHITE;
            if (whiteTeam.king.space.isBeingAttackedByBlack) {
                whiteTeam.filterToOutOfCheckMoves();
            }
        }
    }
}

public enum Colour {
    WHITE,
    BLACK
};

public enum Direction {
    HORIZONTAL,
    VERTICAL,
    POSITIVE,
    NEGATIVE
};