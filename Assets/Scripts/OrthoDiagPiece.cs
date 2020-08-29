using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OrthoDiagPiece : Piece {
    public bool isOrtho;
    public bool isDiag;

    public OrthoDiagPiece(Colour colour) : base(colour) { }

    public override void getPlayableMoves() {
        if (space != null) {
            playableMoves.Clear();

            if (isOrtho) {
                // Down file
                orthoChecks(true, -1);

                // Up file
                orthoChecks(true, 1);

                // Down rank
                orthoChecks(false, -1);

                // Up rank
                orthoChecks(false, 1);
            }

            if (isDiag) {
                // Up file, up rank
                diagChecks(1, 1);

                // Up file, down rank
                diagChecks(1, -1);

                // Down file, down rank
                diagChecks(-1, -1);

                // Down file, up rank
                diagChecks(-1, 1);
            }
        }
    }

    private void orthoChecks(bool isFile, int incrementDirection) {
        int file = space.file;
        int rank = space.rank;
        Piece possiblyPinnedPiece = null;
        Space spaceObserved;
        int spaceObservedNum;
        Direction moveDirection;
        if (isFile) {
            spaceObservedNum = file + incrementDirection;
            moveDirection = Direction.HORIZONTAL;
        }
        else {
            spaceObservedNum = rank + incrementDirection;
            moveDirection = Direction.VERTICAL;
        }
        bool observedEnemyPiece = false;

        while(inBoardRange(spaceObservedNum)) {
            if (isFile) {
                spaceObserved = board[spaceObservedNum, rank];
            }
            else {
                spaceObserved = board[file, spaceObservedNum];
            }

            if (!observedEnemyPiece) {
                spaceObserved.setBeingAttacked(colour);

                if (spaceObserved.isEmpty) {
                    playableMoves.Add(new OrthoDiagMove(this, spaceObserved, moveDirection));
                }
                else {
                    if (spaceObserved.piece.colour != colour) {
                        playableMoves.Add(new OrthoDiagMove(this, spaceObserved, moveDirection));
                        possiblyPinnedPiece = spaceObserved.piece;
                        observedEnemyPiece = true;
                    }
                    else {
                        break;
                    }
                }
            }
            else {
                if (!spaceObserved.isEmpty) {
                    if (spaceObserved.piece is King && spaceObserved.piece.colour != colour) {
                        possiblyPinnedPiece.pin = new Pin(moveDirection, Board.turnNum);
                    }
                    break;
                }
            }

            spaceObservedNum = spaceObservedNum + incrementDirection;
        }
    }

    private void diagChecks(int fileDirection, int rankDirection) {
        int observedFileNum = space.file + fileDirection;
        int observedRankNum = space.rank + rankDirection;
        Piece possiblyPinnedPiece = null;
        Space spaceObserved;
        Direction moveDirection;
        if (Math.Sign(fileDirection) == Math.Sign(rankDirection)) {
            moveDirection = Direction.POSITIVE;
        }
        else {
            moveDirection = Direction.NEGATIVE;
        }
        bool observedEnemyPiece = false;

        while (inBoardRange(observedFileNum) && inBoardRange(observedRankNum)) {
            spaceObserved = board[observedFileNum, observedRankNum];

            if (!observedEnemyPiece) {
                spaceObserved.setBeingAttacked(colour);

                if (spaceObserved.isEmpty) {
                    playableMoves.Add(new OrthoDiagMove(this, spaceObserved, moveDirection));
                }
                else {
                    if (spaceObserved.piece.colour != colour) {
                        playableMoves.Add(new OrthoDiagMove(this, spaceObserved, moveDirection));
                        possiblyPinnedPiece = spaceObserved.piece;
                        observedEnemyPiece = true;
                    }
                    else {
                        break;
                    }
                }
            }
            else {
                if (!spaceObserved.isEmpty) {
                    if (spaceObserved.piece is King && spaceObserved.piece.colour != colour) {
                        possiblyPinnedPiece.pin = new Pin(moveDirection, Board.turnNum);
                    }
                    break;
                }
            }

            observedFileNum = observedFileNum + fileDirection;
            observedRankNum = observedRankNum + rankDirection;
        }
    }

    public override void filterPlayableMoves() {
        if (pin != null) {
            if (pin.turnPinned == Board.turnNum) {
                List<Move> movesToRemove = new List<Move>();
                foreach (OrthoDiagMove move in playableMoves) {
                    if (move.direction != pin.pinType) {
                        movesToRemove.Add(move);
                    }
                }

                foreach (Move move in movesToRemove) {
                    playableMoves.Remove(move);
                }
            }
        }
        else {
            pin = null;
        }
    }
}
