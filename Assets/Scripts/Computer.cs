using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.UIElements.GraphView;
using UnityEngine;

public class Computer {
    public Colour colour;
    public int maxLevel;

    public Computer(Colour colour, int maxLevel) {
        this.colour = colour;
        this.maxLevel = maxLevel;
    }

    public void move() {
        TreeNode zero = new TreeNode(0);
        miniMax(zero, -999999, 999999);

        List<Move> possibleMoves = new List<Move>();
        foreach (TreeNode treeNode in zero.branchingNodes) {
            if (treeNode.value == zero.value) {
                possibleMoves.Add(treeNode.move);
            }
        }


        // Prioritise pawn moves if there are few pieces.
        if (Board.getOpposingTeamFromColour(colour).alivePieces.Count <= 8) {
            bool pawnMoveIsPossible = false;
            foreach (Move move in possibleMoves) {
                if (move is PawnMove) {
                    pawnMoveIsPossible = true;
                }
            }

            if (pawnMoveIsPossible) {
                Debug.Log("prioritising pawn move");
                List<Move> nonPawnMoves = new List<Move>();
                foreach (Move move in possibleMoves) {
                    if (!(move is PawnMove)) {
                        nonPawnMoves.Add(move);
                    }
                }

                foreach (Move nonPawnMove in nonPawnMoves) {
                    possibleMoves.Remove(nonPawnMove);
                }
            }
        }

        int randomIndex = Random.Range(0, possibleMoves.Count);
        bool pawnMoveOrPieceTaken = possibleMoves[randomIndex].executeRealMove();

        Board.updateFiftyMoveRule(pawnMoveOrPieceTaken);
        GameEvents.changeTurn.Invoke();
        GameObject.Find("chess manager").GetComponent<ChessDisplayManager>().updateBoardDisplay();
    }

    public void miniMax(TreeNode node, float alpha, float beta) {
        node.alpha = alpha;
        node.beta = beta;

        if (node.level == maxLevel || Board.gameIsOver || Board.gameIsStalemate) {
            node.evaluateBoardValue();
        }
        else {
            node.getBranchingNodes();

            foreach (TreeNode treeNode in node.branchingNodes) {
                treeNode.move.executeMove();
                Board.softChangeTurn();

                miniMax(treeNode, node.alpha, node.beta);

                treeNode.move.undoMove();
                Board.softChangeTurn();

                if (Board.turn == Colour.WHITE) {
                    if (treeNode.value > node.alpha) {
                        node.alpha = treeNode.value;
                    }
                }
                else {
                    if (treeNode.value < node.beta) {
                        node.beta = treeNode.value;
                    }
                }

                if (node.beta < node.alpha) {
                    break;
                }
            }

            node.evaluateBranchValues();
            if (node.level != 0) {
                node.branchingNodes = null;
            }
        }
    }
}
