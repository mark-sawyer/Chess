using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.UIElements.GraphView;
using UnityEngine;

public class Computer {
    public static Colour colour = Colour.BLACK;
    public static int maxLevel = 3;
    public static int iterations;

    public static void move() {
        TreeNode zero = new TreeNode(0);
        miniMax(zero, -999999, 999999);

        List<Move> possibleMoves = new List<Move>();
        foreach (TreeNode treeNode in zero.branchingNodes) {
            if (treeNode.value == zero.value) {
                possibleMoves.Add(treeNode.move);
            }
        }

        Debug.Log("" + iterations);

        int randomIndex = Random.Range(0, possibleMoves.Count);
        possibleMoves[randomIndex].executeMove();
        GameEvents.changeTurn.Invoke();
        GameObject.Find("chess manager").GetComponent<ChessDisplayManager>().updateBoardDisplay();
    }

    public static void miniMax(TreeNode node, int alpha, int beta) {
        iterations++;

        node.alpha = alpha;
        node.beta = beta;

        if (node.level == maxLevel || Board.gameIsOver) {
            node.evaluateBoardValue();
        }
        else {
            node.getBranchingNodes();

            foreach (TreeNode treeNode in node.branchingNodes) {
                treeNode.alpha = node.alpha;
                treeNode.beta = node.beta;

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
