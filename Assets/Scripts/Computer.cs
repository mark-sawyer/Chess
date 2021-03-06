﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer {
    public static int WORST_WHITE_SCORE = -999999;
    public static int WORST_BLACK_SCORE = 999999;
    public Colour colour;
    public int maxLevel;

    public Computer(Colour colour, int maxLevel) {
        this.colour = colour;
        this.maxLevel = maxLevel;
    }

    public void move() {
        TreeNode zero = new TreeNode(0);
        miniMax(zero, WORST_WHITE_SCORE, WORST_BLACK_SCORE);

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
                    break;
                }
            }

            if (pawnMoveIsPossible) {
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
            node.evaluateBoardValue(node.level);
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
        }
    }
}
