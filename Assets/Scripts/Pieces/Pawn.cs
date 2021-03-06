﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece {
    public int direction;
    public int turnMovedTwo;
    public Queen promotionQueen;
    public bool isPromoted;

    public Pawn(Colour colour) : base(colour) {
        GameEvents.getPlayableMoves.AddListener(getPlayableMoves);
        value = 1;

        if (colour == Colour.WHITE) {
            direction = 1;
        }
        else {
            direction = -1;
            value *= -1;
        }

        promotionQueen = new Queen(colour);
        promotionQueen.removeListeners();
        promotionQueen.isHost = false;
        promotionQueen.host = this;
        turnMovedTwo = -999;
    }

    public override void getPlayableMoves() {
        if (space != null) {
            if (!isPromoted) {
                playableMoves.Clear();

                int file = space.file;
                int rank = space.rank;

                // Add space ahead if empty.
                if (rank % 7 >= 1 && board[file, rank + direction].isEmpty) {
                    playableMoves.Add(new PawnMove(this, board[file, rank + direction]));

                    // Add two spaces ahead if empty and the pawn hasn't moved.
                    if (timesMoved == 0 && board[file, rank + (2 * direction)].isEmpty) {
                        playableMoves.Add(new PawnMove(this, board[file, rank + (2 * direction)]));
                    }
                }

                // Add diagonals.
                Space spaceObserved;
                // Lower file
                if (rank % 7 >= 1 && file >= 1) {
                    spaceObserved = board[file - 1, rank + direction];
                    spaceObserved.setBeingAttacked(colour);
                    if (!spaceObserved.isEmpty && spaceObserved.piece.colour != colour) {
                        if (colour == Colour.WHITE) {
                            playableMoves.Add(new DiagonalPawnMove(this, spaceObserved, Direction.NEGATIVE));
                        }
                        else {
                            playableMoves.Add(new DiagonalPawnMove(this, spaceObserved, Direction.POSITIVE));
                        }
                    }

                    // En passant
                    else if ((colour == Colour.WHITE && rank == 4) || (colour == Colour.BLACK && rank == 3)) {
                        spaceObserved = board[file - 1, rank];
                        if (!spaceObserved.isEmpty && spaceObserved.piece.colour != colour && spaceObserved.piece is Pawn &&
                            ((Pawn)spaceObserved.piece).timesMoved == 1 && ((Pawn)spaceObserved.piece).turnMovedTwo == Board.turnNum - 1) {
                            if (colour == Colour.WHITE) {
                                playableMoves.Add(new EnPassantMove(this, spaceObserved, Direction.NEGATIVE));
                            }
                            else {
                                playableMoves.Add(new EnPassantMove(this, spaceObserved, Direction.POSITIVE));
                            }
                        }
                    }
                }

                // Higher file
                if (rank % 7 >= 1 && file <= 6) {
                    spaceObserved = board[file + 1, rank + direction];
                    spaceObserved.setBeingAttacked(colour);
                    if (!spaceObserved.isEmpty && spaceObserved.piece.colour != colour) {
                        if (colour == Colour.WHITE) {
                            playableMoves.Add(new DiagonalPawnMove(this, spaceObserved, Direction.POSITIVE));
                        }
                        else {
                            playableMoves.Add(new DiagonalPawnMove(this, spaceObserved, Direction.NEGATIVE));
                        }
                    }

                    // En passant
                    else if ((colour == Colour.WHITE && rank == 4) || (colour == Colour.BLACK && rank == 3)) {
                        spaceObserved = board[file + 1, rank];
                        if (!spaceObserved.isEmpty && spaceObserved.piece.colour != colour && spaceObserved.piece is Pawn &&
                            ((Pawn)spaceObserved.piece).timesMoved == 1 && ((Pawn)spaceObserved.piece).turnMovedTwo == Board.turnNum - 1) {
                            if (colour == Colour.WHITE) {
                                playableMoves.Add(new EnPassantMove(this, spaceObserved, Direction.POSITIVE));
                            }
                            else {
                                playableMoves.Add(new EnPassantMove(this, spaceObserved, Direction.NEGATIVE));
                            }
                        }
                    }
                }
            }
            else {
                promotionQueen.getPlayableMoves();
                playableMoves = promotionQueen.playableMoves;
            }
        }
    }

    public override GameObject getGameObject() {
        if (!isPromoted) {
            if (colour == Colour.WHITE) {
                return Resources.Load<GameObject>("White/white pawn");
            }
            else {
                return Resources.Load<GameObject>("Black/black pawn");
            }
        }
        else {
            if (colour == Colour.WHITE) {
                return Resources.Load<GameObject>("White/white queen");
            }
            else {
                return Resources.Load<GameObject>("Black/black queen");
            }
        }
    }

    public override void filterPlayableMoves() {
        if (space != null) {
            if (!isPromoted && pin != null) {
                List<Move> movesToRemove = new List<Move>();
                switch (pin.pinType) {
                    case Direction.HORIZONTAL:
                        playableMoves.Clear();
                        break;

                    case Direction.VERTICAL:
                        foreach (Move move in playableMoves) {
                            if (move is DiagonalPawnMove) {
                                movesToRemove.Add(move);
                            }
                        }

                        foreach (Move move in movesToRemove) {
                            playableMoves.Remove(move);
                        }

                        break;

                    case Direction.POSITIVE:
                        foreach (Move move in playableMoves) {
                            if (!(move is DiagonalPawnMove) || ((DiagonalPawnMove)move).direction != pin.pinType) {
                                movesToRemove.Add(move);
                            }
                        }

                        foreach (Move move in movesToRemove) {
                            playableMoves.Remove(move);
                        }
                        break;

                    case Direction.NEGATIVE:
                        foreach (Move move in playableMoves) {
                            if (!(move is DiagonalPawnMove) || ((DiagonalPawnMove)move).direction != pin.pinType) {
                                movesToRemove.Add(move);
                            }
                        }

                        foreach (Move move in movesToRemove) {
                            playableMoves.Remove(move);
                        }
                        break;
                }

                pin = null;
            }
            else if (isPromoted && promotionQueen.pin != null) {
                promotionQueen.filterPlayableMoves();
            }
        }
    }
}
