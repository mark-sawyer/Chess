using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Piece {
    public Space[,] board;
    public Space space;
    public List<Space> reachableSpaces;
    public Colour colour;
    public int value;

    public Piece(Space space, Colour colour) {
        this.space = space;
        this.colour = colour;
        board = Board.board;
        reachableSpaces = new List<Space>();

        GameEvents.getReachableSpaces.AddListener(getReachableSpaces);
    }

    public virtual void setPosition(Space newSpace) {
        // Check if a piece is being taken.
        if (!newSpace.isEmpty) {
            Piece removedPiece = newSpace.piece;
            newSpace.removePiece();

            if (removedPiece.colour == Colour.WHITE) {
                Board.aliveWhitePieces.Remove(removedPiece);
            }
            else {
                Board.aliveBlackPieces.Remove(removedPiece);
            }
        }

        space.removePiece();
        space = newSpace;
        space.setPiece(this);

        GameEvents.changeTurn.Invoke();
    }

    public abstract void getReachableSpaces();

    public abstract GameObject getGameObject();
}
