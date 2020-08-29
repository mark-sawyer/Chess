using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Piece {
    public Space[,] board;
    public Space space;
    public Team team;
    public List<Move> playableMoves;
    public Pin pin;
    public Colour colour;
    public int value;
    public int timesMoved;

    public Piece(Colour colour) {
        GameEvents.setTeam.AddListener(setTeam);
        GameEvents.getPlayableMoves.AddListener(getPlayableMoves);
        GameEvents.filterPlayableMoves.AddListener(filterPlayableMoves);

        this.colour = colour;
        board = Board.board;
        playableMoves = new List<Move>();
        timesMoved = 0;
    }

    public abstract void getPlayableMoves();

    public abstract GameObject getGameObject();

    public bool inBoardRange(int num) {
        return num >= 0 && num <= 7;
    }

    private void setTeam() {
        if (colour == Colour.WHITE) {
            team = Board.whiteTeam;
        }
        else {
            team = Board.blackTeam;
        }
    }

    public Move getMoveMatchingToSpace(Space space) {
        foreach (Move move in playableMoves) {
            if (move.newSpace == space) {
                return move;
            }
        }
        return null;
    }

    public abstract void filterPlayableMoves();
}
