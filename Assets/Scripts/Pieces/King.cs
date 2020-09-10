using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece {
    public King(Colour colour) : base(colour) { }

    public override void getPlayableMoves() {
        if (space != null) {
            playableMoves.Clear();
            int file = space.file;
            int rank = space.rank;

            setReaching(file, rank + 1);
            setReaching(file + 1, rank + 1);
            setReaching(file + 1, rank);
            setReaching(file + 1, rank - 1);
            setReaching(file, rank - 1);
            setReaching(file - 1, rank - 1);
            setReaching(file - 1, rank);
            setReaching(file - 1, rank + 1);

            // Castling
            if (timesMoved == 0) {
                if (colour == Colour.WHITE) {
                    // White long
                    if (!board[0, 0].isEmpty && board[0, 0].piece is Rook && board[0, 0].piece.timesMoved == 0 &&
                        board[1, 0].isEmpty && board[2, 0].isEmpty && board[3, 0].isEmpty) {

                        playableMoves.Add(new CastlingMove(CastlingType.WHITE_LONG, this, board[2, 0]));
                    }

                    // White short
                    if (!board[7, 0].isEmpty && board[7, 0].piece is Rook && board[7, 0].piece.timesMoved == 0 &&
                        board[5, 0].isEmpty && board[6, 0].isEmpty) {

                        playableMoves.Add(new CastlingMove(CastlingType.WHITE_SHORT, this, board[6, 0]));
                    }

                }
                else if (colour == Colour.BLACK) {
                    // Black long
                    if (!board[0, 7].isEmpty && board[0, 7].piece is Rook && board[0, 7].piece.timesMoved == 0 &&
                        board[1, 7].isEmpty && board[2, 7].isEmpty && board[3, 7].isEmpty) {

                        playableMoves.Add(new CastlingMove(CastlingType.BLACK_LONG, this, board[2, 7]));
                    }

                    // Black short
                    if (!board[7, 7].isEmpty && board[7, 7].piece is Rook && board[7, 7].piece.timesMoved == 0 &&
                        board[5, 7].isEmpty && board[6, 7].isEmpty) {

                        playableMoves.Add(new CastlingMove(CastlingType.BLACK_SHORT, this, board[6, 7]));
                    }
                }
            }
        }
    }

    public override GameObject getGameObject() {
        if (colour == Colour.WHITE) {
            return Resources.Load<GameObject>("White/white king");
        }
        else {
            return Resources.Load<GameObject>("Black/black king");
        }
    }

    private void setReaching(int goalFile, int goalRank) {
        if (inBoardRange(goalFile) && inBoardRange(goalRank)) {
            Space spaceObserved = board[goalFile, goalRank];
            spaceObserved.setBeingAttacked(colour);
            if (spaceObserved.isEmpty || spaceObserved.piece.colour != colour) {
                playableMoves.Add(new Move(this, spaceObserved));
            }
        }
    }

    public override void filterPlayableMoves() {
        List<Move> movesToRemove = new List<Move>();
        foreach (Move move in playableMoves) {
            if (colour == Colour.WHITE) {
                if (!(move is CastlingMove)) {
                    if (move.newSpace.isBeingAttackedByBlack) {
                        movesToRemove.Add(move);
                    }
                }
                else {
                    if (((CastlingMove)move).castlingType == CastlingType.WHITE_LONG) {
                        if (space.isBeingAttackedByBlack || board[2, 0].isBeingAttackedByBlack || board[3, 0].isBeingAttackedByBlack) {
                            movesToRemove.Add(move);
                        }
                    }
                    else {
                        if (space.isBeingAttackedByBlack || board[5, 0].isBeingAttackedByBlack || board[6, 0].isBeingAttackedByBlack) {
                            movesToRemove.Add(move);
                        }
                    }
                }                
            }
            else {
                if (!(move is CastlingMove)) {
                    if (move.newSpace.isBeingAttackedByWhite) {
                        movesToRemove.Add(move);
                    }
                }
                else {
                    if (((CastlingMove)move).castlingType == CastlingType.BLACK_LONG) {
                        if (space.isBeingAttackedByWhite || board[2, 7].isBeingAttackedByWhite || board[3, 7].isBeingAttackedByWhite) {
                            movesToRemove.Add(move);
                        }
                    }
                    else {
                        if (space.isBeingAttackedByWhite || board[5, 7].isBeingAttackedByWhite || board[6, 7].isBeingAttackedByWhite) {
                            movesToRemove.Add(move);
                        }
                    }
                }
            }
        }

        foreach (Move move in movesToRemove) {
            playableMoves.Remove(move);
        }
    }
}
