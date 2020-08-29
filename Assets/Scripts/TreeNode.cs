using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeNode {
    public int level;
    public int value;
    public Move move;
    public List<TreeNode> branchingNodes;

    public TreeNode(int level) {
        this.level = level;
        branchingNodes = new List<TreeNode>();
    }

    public TreeNode(int level, Move move) {
        this.level = level;
        this.move = move;
        branchingNodes = new List<TreeNode>();
    }

    public void getBranchingNodes() {
        int totalPlayableMoves = 0;
        Team playingTeam;

        if (Board.turn == Colour.WHITE) {
            playingTeam = Board.whiteTeam;
        }
        else {
            playingTeam = Board.blackTeam;
        }

        // Find the appropriate capacity
        foreach (Piece piece in playingTeam.alivePieces) {
            totalPlayableMoves += piece.playableMoves.Count;
        }
        branchingNodes = new List<TreeNode>(totalPlayableMoves);

        foreach (Piece piece in playingTeam.alivePieces) {
            foreach (Move move in piece.playableMoves) {
                branchingNodes.Add(new TreeNode(level + 1, move));
            }
        }
    }

    public int evaluateBoard() {
        int value = 0;

        foreach (Piece piece in Board.whiteTeam.alivePieces) {
            value += piece.value;
        }
        foreach (Piece piece in Board.blackTeam.alivePieces) {
            value += piece.value;
        }

        branchingNodes = null;
        return value;
    }

    public int evaluateBranchValues(Colour teamTurn) {
        int num;
        if (teamTurn == Colour.WHITE) {
            num = -9999;
            foreach (TreeNode branchNode in branchingNodes) {
                if (branchNode.value > num) {
                    num = branchNode.value;
                }
            }
        }
        else {
            num = 9999;
            foreach (TreeNode branchNode in branchingNodes) {
                if (branchNode.value < num) {
                    num = branchNode.value;
                }
            }
        }

        if (level != 0) {
            branchingNodes = null;
        }

        return num;
    }
}
