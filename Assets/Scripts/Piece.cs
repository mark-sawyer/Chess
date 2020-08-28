using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Piece {
    public Space[,] board;
    public Space space;
    public Team team;
    public List<Space> reachableSpaces;
    public Pin pin;
    public Colour colour;
    public int value;

    public Piece(Space space, Colour colour) {
        GameEvents.setTeam.AddListener(setTeam);

        this.space = space;
        this.colour = colour;
        board = Board.board;
        reachableSpaces = new List<Space>();
    }

    public virtual void setPosition(Space newSpace) {
        // Check if a piece is being taken.
        if (!newSpace.isEmpty) {
            Piece removedPiece = newSpace.piece;
            newSpace.removePiece();
            team.alivePieces.Remove(removedPiece);
        }

        space.removePiece();
        space = newSpace;
        space.setPiece(this);

        GameEvents.changeTurn.Invoke();
    }

    public abstract void getReachableSpaces();

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
}
