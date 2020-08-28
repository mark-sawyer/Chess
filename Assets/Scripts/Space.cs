using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Space {
    public Piece piece;
    public bool isEmpty;
    public bool isBeingAttackedByWhite;
    public bool isBeingAttackedByBlack;
    public int file, rank;

    public Space(int file, int rank) {
        GameEvents.clearBeingAttacked.AddListener(clearBeingAttacked);

        this.file = file;
        this.rank = rank;
        isEmpty = true;
    }

    public void setPiece(Piece newPiece) {
        piece = newPiece;
        isEmpty = false;
    }

    public void removePiece() {
        piece.space = null;
        piece = null;
        isEmpty = true;
    }

    public void setBeingAttacked(Colour colour) {
        if (colour == Colour.WHITE) {
            isBeingAttackedByWhite = true;
        }
        else {
            isBeingAttackedByBlack = true;
        }
    }

    private void clearBeingAttacked() {
        isBeingAttackedByWhite = false;
        isBeingAttackedByBlack = false;
    }
}
