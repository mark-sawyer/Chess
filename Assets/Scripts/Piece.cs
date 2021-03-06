﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Piece {
    public Space[,] board;
    public Space space;
    public Space initialSpace;
    public Team team;
    public List<Move> playableMoves;
    public Pin pin;
    public Colour colour;
    public float value;
    public int timesMoved;
    public bool isHost;

    public Piece(Colour colour) {
        isHost = true;
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

    public void setTeam() {
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

    public void resetPiece() {
        space.removePiece();
        space = initialSpace;
    }
}
