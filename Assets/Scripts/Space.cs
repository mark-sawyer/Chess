using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Space {
    public Piece piece;
    public bool isEmpty;
    public int file, rank;

    public Space(int file, int rank) {
        this.file = file;
        this.rank = rank;
        isEmpty = true;
    }

    public void setPiece(Piece newPiece) {
        piece = newPiece;
        isEmpty = false;
    }

    public void removePiece() {
        piece = null;
        isEmpty = true;
    }
}
