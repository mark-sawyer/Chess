using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastlingMove : Move {
    public CastlingType castlingType;
    Space newRookSpace;
    Space oldRookSpace;
    Piece rook;

    public CastlingMove(CastlingType castlingType, Piece movingPiece, Space newSpace) : base(movingPiece, newSpace) {
        this.castlingType = castlingType;
    }

    public override void executeMove() {
        switch (castlingType) {
            case CastlingType.WHITE_LONG:
                rook = Board.board[0, 0].piece;
                newRookSpace = Board.board[3, 0];
                break;
            case CastlingType.WHITE_SHORT:
                rook = Board.board[7, 0].piece;
                newRookSpace = Board.board[5, 0];
                break;
            case CastlingType.BLACK_LONG:
                rook = Board.board[0, 7].piece;
                newRookSpace = Board.board[3, 7];
                break;
            case CastlingType.BLACK_SHORT:
                rook = Board.board[7, 7].piece;
                newRookSpace = Board.board[5, 7];
                break;
        }

        oldRookSpace = rook.space;

        oldSpace.removePiece();
        newSpace.setPiece(movingPiece);
        oldRookSpace.removePiece();
        newRookSpace.setPiece(rook);
        movingPiece.timesMoved++;
        rook.timesMoved++;
        Board.turnNum++;
    }

    public override void undoMove() {
        newSpace.removePiece();
        oldSpace.setPiece(movingPiece);
        newRookSpace.removePiece();
        oldRookSpace.setPiece(rook);

        movingPiece.timesMoved--;
        rook.timesMoved--;
        Board.turnNum--;
        Board.gameIsOver = false;
        Board.gameIsStalemate = false;
    }
}

public enum CastlingType {
    WHITE_LONG,
    WHITE_SHORT,
    BLACK_LONG,
    BLACK_SHORT
};