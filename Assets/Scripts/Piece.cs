using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Piece {
    public Space space;
    public Colour colour;

    public Piece(Space space, Colour colour) {
        this.space = space;
        this.colour = colour;
    }

    public abstract void move();

    public void setPosition(int file, int rank) {
        // Reset data for current space.
        space.removePiece();

        // Set data for new space.
        space = Board.board[file, rank];
        space.setPiece(this);
    }
}

public enum Colour {
    BLACK,
    WHITE
};