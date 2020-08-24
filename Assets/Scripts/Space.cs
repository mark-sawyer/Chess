using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Space {
    public Piece piece;
    public bool isEmpty;
    public int[,] coordinates;

    public Space(int[,] coordinates) {
        this.coordinates = coordinates;
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
