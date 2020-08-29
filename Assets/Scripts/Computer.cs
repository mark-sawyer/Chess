using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.UIElements.GraphView;
using UnityEngine;

public class Computer {
    public static Colour colour = Colour.BLACK;
    public static int maxLevel = 4;
    public static int iteration = 0;

    public static void move() {
        TreeNode zero = new TreeNode(0);
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
        iteration++;
        if (node.level == maxLevel || Board.gameIsOver) {
            node.evaluateBoardValue();
        }
        else {
            node.getBranchingNodes();

            foreach (TreeNode treeNode in node.branchingNodes) {
                /*if (Board.board[4, 7].piece is King && Board.board[1, 4].piece is Bishop && Board.board[4, 3].piece is Pawn && Board.board[3, 5].piece is Pawn &&
                    treeNode.move.movingPiece is King && treeNode.move.newSpace.file == 3 && treeNode.move.newSpace.rank == 6) {
                    int x = Computer.iteration;
                    x = x + 0;
                }*/

                treeNode.move.executeMove();
                Board.softChangeTurn();

                miniMax(treeNode);

                treeNode.move.undoMove();
                Board.softChangeTurn();
            }

            node.evaluateBranchValues();
            if (node.level != 0) {
                node.branchingNodes = null;
            }
        }
    }
}
