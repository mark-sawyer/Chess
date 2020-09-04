using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
    public static Space[,] board = new Space[8, 8];
    public static Colour turn;
    public static Team whiteTeam;
    public static Team blackTeam;
    public static int turnNum;
    public static bool gameIsOver;
    public static bool gameIsStalemate;
    public static bool whiteIsAI = false;
    public static bool blackIsAI = false;
    public static Computer whiteComputer;
    public static Computer blackComputer;
    public static int fiftyMoveRule;

    private void Awake() {
        GameEvents.changeTurn.AddListener(changeTurn);
        whiteComputer = new Computer(Colour.WHITE, 3);
        blackComputer = new Computer(Colour.BLACK, 3);

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

    private void Start() {
        GameObject.Find("chess manager").GetComponent<ChessDisplayManager>().start();
        if (whiteIsAI) {
            ComputerTimer.willPlay = true;
        }
    }

    public static void changeTurn() {
        GameEvents.clearBeingAttacked.Invoke();
        GameEvents.getPlayableMoves.Invoke();
        GameEvents.filterPlayableMoves.Invoke();

        if (turn == Colour.WHITE) {
            turn = Colour.BLACK;
            if (blackTeam.king.space.isBeingAttackedByWhite) {
                GameObject.Find("chess manager").GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("click"));
                gameIsOver = blackTeam.isCheckmated();
            }
            else {
                gameIsStalemate = blackTeam.isStalemated();
            }
        }
        else {
            turn = Colour.WHITE;
            if (whiteTeam.king.space.isBeingAttackedByBlack) {
                GameObject.Find("chess manager").GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("click"));
                gameIsOver = whiteTeam.isCheckmated();
            }
            else {
                gameIsStalemate = whiteTeam.isStalemated();
            }
        }

        if (gameIsOver || gameIsStalemate || fiftyMoveRule == 50) {
            GameObject.Find("end of game message").GetComponent<EndOfGameMessage>().turnOnMessage();
            GameObject.Find("turn indicator").GetComponent<SpriteRenderer>().enabled = false;
        }
        else if ((turn == Colour.WHITE && whiteIsAI) || (turn == Colour.BLACK && blackIsAI))  {
            ComputerTimer.willPlay = true;
        }
    }

    public static void softChangeTurn() {
        GameEvents.clearBeingAttacked.Invoke();
        GameEvents.getPlayableMoves.Invoke();
        GameEvents.filterPlayableMoves.Invoke();

        if (turn == Colour.WHITE) {
            turn = Colour.BLACK;
            if (blackTeam.king.space.isBeingAttackedByWhite) {
                gameIsOver = blackTeam.isCheckmated();
            }
            else {
                gameIsStalemate = blackTeam.isStalemated();
            }
        }
        else {
            turn = Colour.WHITE;
            if (whiteTeam.king.space.isBeingAttackedByBlack) {
                gameIsOver = whiteTeam.isCheckmated();
            }
            else {
                gameIsStalemate = whiteTeam.isStalemated();
            }
        }
    }

    public static void startGame() {
        GameObject.Find("turn indicator").GetComponent<SpriteRenderer>().enabled = true;
        fiftyMoveRule = 0;
        gameIsOver = false;
        gameIsStalemate = false;
        turnNum = 1;
        whiteTeam.resetTeam();
        blackTeam.resetTeam();
        turn = Colour.WHITE;

        GameObject.Find("chess manager").GetComponent<ChessDisplayManager>().enabled = true;
        GameObject.Find("chess manager").GetComponent<ChessDisplayManager>().updateBoardDisplay();
        GameObject.Find("turn indicator").GetComponent<TurnIndicator>().setToWhite();

        GameEvents.clearBeingAttacked.Invoke();
        GameEvents.getPlayableMoves.Invoke();
        GameEvents.filterPlayableMoves.Invoke();
        if (whiteIsAI) {
            ComputerTimer.willPlay = true;
        }
    }

    public static void endGame() {
        ComputerTimer.abort();
        GameObject.Find("end of game message").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("turn indicator").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("chess manager").GetComponent<ChessDisplayManager>().enabled = false;
    }

    public static void updateFiftyMoveRule(bool update) {
        if (turn == Colour.WHITE) {
            whiteTeam.pawnMoveOrPieceTakenOnTurn = update;
        }
        else {
            blackTeam.pawnMoveOrPieceTakenOnTurn = update;

            if (!whiteTeam.pawnMoveOrPieceTakenOnTurn && !blackTeam.pawnMoveOrPieceTakenOnTurn) {
                fiftyMoveRule++;
            }
            else {
                fiftyMoveRule = 0;
            }
        }
    }

    public static Team getOpposingTeamFromColour(Colour teamColour) {
        if (teamColour == Colour.WHITE) {
            return blackTeam;
        }
        else {
            return whiteTeam;
        }
    }

    public static Computer getComputerFromColour(Colour computerColour) {
        if (computerColour == Colour.WHITE) {
            return whiteComputer;
        }
        else {
            return blackComputer;
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