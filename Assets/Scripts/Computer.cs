using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.UIElements.GraphView;
using UnityEngine;

public class Computer {
    public static Colour colour = Colour.BLACK;
    public static int maxLevel = 1;
    public static int breakNum;

    public static void move() {
        TreeNode zero = new TreeNode(0, -999999, 999999);
        miniMax(zero);

        List<Move> possibleMoves = new List<Move>();
        foreach (TreeNode treeNode in zero.branchingNodes) {
            if (treeNode.value == zero.value) {
                possibleMoves.Add(treeNode.move);
            }
        }

        int randomIndex = Random.Range(0, possibleMoves.Count);
        possibleMoves[randomIndex].executeMove();
        GameEvents.changeTurn.Invoke();
        GameObject.Find("chess manager").GetComponent<ChessDisplayManager>().updateBoardDisplay();
    }

    public static void miniMax(TreeNode node) {
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

                miniMax(treeNode);

                treeNode.move.undoMove();
                Board.softChangeTurn();

                if (Board.turn == Colour.WHITE) {
                    if (treeNode.value > node.alpha) {
                        node.alpha = treeNode.alpha;
                    }
                }
                else {
                    if (treeNode.value < node.beta) {
                        node.beta = treeNode.beta;
                    }
                }

                if (node.beta < node.alpha) {
                    breakNum++;
                    Debug.Log(breakNum);
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
