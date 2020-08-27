using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OrthoDiagPiece : Piece {
    public bool isOrtho;
    public bool isDiag;

    public OrthoDiagPiece(Space space, Colour colour) : base(space, colour) { }

    public override void getReachableSpaces() {
        reachableSpaces.Clear();

        int file = space.file;
        int rank = space.rank;
        Space spaceObserved;

        if (isOrtho) {
            int spaceObservedNum;

            // Down file
            spaceObservedNum = file - 1;
            while (inBoardRange(spaceObservedNum)) {
                spaceObserved = board[spaceObservedNum, rank];
                spaceObserved.setBeingAttacked(colour);

                if (spaceObserved.isEmpty) {
                    reachableSpaces.Add(spaceObserved);
                }
                else {
                    if (spaceObserved.piece.colour != colour) {
                        reachableSpaces.Add(spaceObserved);
                    }
                    break;
                }
                spaceObservedNum--;
            }

            // Up file
            spaceObservedNum = file + 1;
            while (inBoardRange(spaceObservedNum)) {
                spaceObserved = board[spaceObservedNum, rank];
                spaceObserved.setBeingAttacked(colour);

                if (spaceObserved.isEmpty) {
                    reachableSpaces.Add(spaceObserved);
                }
                else {
                    if (spaceObserved.piece.colour != colour) {
                        reachableSpaces.Add(spaceObserved);
                    }
                    break;
                }
                spaceObservedNum++;
            }

            // Down rank
            spaceObservedNum = rank - 1;
            while (spaceObservedNum >= 0) {
                spaceObserved = board[file, spaceObservedNum];
                spaceObserved.setBeingAttacked(colour);

                if (spaceObserved.isEmpty) {
                    reachableSpaces.Add(spaceObserved);
                }
                else {
                    if (spaceObserved.piece.colour != colour) {
                        reachableSpaces.Add(spaceObserved);
                    }
                    break;
                }
                spaceObservedNum--;
            }

            // Up rank
            spaceObservedNum = rank + 1;
            while (spaceObservedNum <= 7) {
                spaceObserved = board[file, spaceObservedNum];
                spaceObserved.setBeingAttacked(colour);

                if (spaceObserved.isEmpty) {
                    reachableSpaces.Add(spaceObserved);
                }
                else {
                    if (spaceObserved.piece.colour != colour) {
                        reachableSpaces.Add(spaceObserved);
                    }
                    break;
                }
                spaceObservedNum++;
            }
        }

        if (isDiag) {
            int observedFileNum;
            int observedRankNum;

            // Up rank, up file
            observedFileNum = file + 1;
            observedRankNum = rank + 1;
            while (observedFileNum <= 7 && observedRankNum <= 7) {
                spaceObserved = board[observedFileNum, observedRankNum];
                spaceObserved.setBeingAttacked(colour);

                if (spaceObserved.isEmpty) {
                    reachableSpaces.Add(spaceObserved);
                }
                else {
                    if (spaceObserved.piece.colour != colour) {
                        reachableSpaces.Add(spaceObserved);
                    }
                    break;
                }
                observedFileNum++;
                observedRankNum++;
            }

            // Down rank, up file
            observedFileNum = file + 1;
            observedRankNum = rank - 1;
            while (observedFileNum <= 7 && observedRankNum >= 0) {
                spaceObserved = board[observedFileNum, observedRankNum];
                spaceObserved.setBeingAttacked(colour);

                if (spaceObserved.isEmpty) {
                    reachableSpaces.Add(spaceObserved);
                }
                else {
                    if (spaceObserved.piece.colour != colour) {
                        reachableSpaces.Add(spaceObserved);
                    }
                    break;
                }
                observedFileNum++;
                observedRankNum--;
            }

            // Down rank, down file
            observedFileNum = file - 1;
            observedRankNum = rank - 1;
            while (observedFileNum >= 0 && observedRankNum >= 0) {
                spaceObserved = board[observedFileNum, observedRankNum];
                spaceObserved.setBeingAttacked(colour);

                if (spaceObserved.isEmpty) {
                    reachableSpaces.Add(spaceObserved);
                }
                else {
                    if (spaceObserved.piece.colour != colour) {
                        reachableSpaces.Add(spaceObserved);
                    }
                    break;
                }
                observedFileNum--;
                observedRankNum--;
            }

            // Up rank, down file
            observedFileNum = file - 1;
            observedRankNum = rank + 1;
            while (observedFileNum >= 0 && observedRankNum <= 7) {
                spaceObserved = board[observedFileNum, observedRankNum];
                spaceObserved.setBeingAttacked(colour);

                if (spaceObserved.isEmpty) {
                    reachableSpaces.Add(spaceObserved);
                }
                else {
                    if (spaceObserved.piece.colour != colour) {
                        reachableSpaces.Add(spaceObserved);
                    }
                    break;
                }
                observedFileNum--;
                observedRankNum++;
            }
        }
    }
}
