using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.UIElements.GraphView;
using UnityEngine;

public class Computer {
    public static Colour colour = Colour.BLACK;
    public static int maxLevel = 2;

    public static void move() {
        TreeNode zero = new TreeNode(0);
        evaluateNode(zero);
    }

    public static void evaluateNode(TreeNode node) {
        // Set the branches of the node
        if (node.level < maxLevel) {
            node.getBranchingNodes();
        }

        // For each branch, execute the move, evaluate the position, and undo the move.
        // Empty for maxLevel node so skipped.
        foreach (TreeNode branchNode in node.branchingNodes) {
            branchNode.move.executeMove();
            Board.softChangeTurn();

            evaluateNode(branchNode);

            branchNode.move.undoMove();
            Board.softChangeTurn();
        }

        if (node.level == maxLevel) {
            node.value = node.evaluateBoard();
        }
        else {
            node.value = node.evaluateBranchValues(Board.turn);
            
            if (node.level == 0) {
                List<Move> possibleMoves = new List<Move>();
                foreach (TreeNode branchNode in node.branchingNodes) {
                    if (branchNode.value == node.value) {
                        possibleMoves.Add(branchNode.move);
                    }
                }

                int randomIndex = Random.Range(0, possibleMoves.Count);
                possibleMoves[randomIndex].executeMove();
                GameEvents.changeTurn.Invoke();
                GameObject.Find("chess manager").GetComponent<ChessDisplayManager>().updateBoardDisplay();
            }
        }
    }
}
