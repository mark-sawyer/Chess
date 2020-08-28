using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece {
    public int direction;
    public bool hasMoved;
    public bool justMovedTwo;

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
    }

    public override void getPlayableMoves() {
        if (justMovedTwo && Board.turn != colour) {
            justMovedTwo = false;
        }

        if (space != null) {
            playableMoves.Clear();
            pin = null;

            int file = space.file;
            int rank = space.rank;

            // Add space ahead if empty.
            if (rank % 7 >= 1 && board[file, rank + direction].isEmpty) {
                playableMoves.Add(new PawnMove(this, board[file, rank + direction]));

                // Add two spaces ahead if empty and the pawn hasn't moved.
                if (!hasMoved && board[file, rank + (2 * direction)].isEmpty) {
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
                    if (!spaceObserved.isEmpty && spaceObserved.piece.colour != colour && spaceObserved.piece is Pawn && ((Pawn)spaceObserved.piece).justMovedTwo) {
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
                    if (!spaceObserved.isEmpty && spaceObserved.piece.colour != colour && spaceObserved.piece is Pawn && ((Pawn)spaceObserved.piece).justMovedTwo) {
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
    }

    public override GameObject getGameObject() {
        if (colour == Colour.WHITE) {
            return Resources.Load<GameObject>("White/white pawn");
        }
        else {
            return Resources.Load<GameObject>("Black/black pawn");
        }
    }

    public override void filterPlayableMoves() {
        if (pin != null) {
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
        }
    }
}
