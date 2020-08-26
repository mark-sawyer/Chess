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

        if (isOrtho) {
            Space spaceObserved;
            int spaceObservedNum;

            // Down file
            spaceObservedNum = file - 1;
            while (spaceObservedNum >= 0) {
                spaceObserved = board[spaceObservedNum, rank];

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
            while (spaceObservedNum <= 7) {
                spaceObserved = board[spaceObservedNum, rank];

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

            if (isDiag) {

            }
        }
    }
}
