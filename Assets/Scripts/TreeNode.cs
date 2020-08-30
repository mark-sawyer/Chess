﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeNode {
    public int level;
    public int value;
    public Move move;
    public List<TreeNode> branchingNodes;
    public int alpha;
    public int beta;

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

    public void evaluateBoardValue() {
        int boardValue;
        if (!Board.gameIsOver) {
            boardValue = 0;
            foreach (Piece piece in Board.whiteTeam.alivePieces) {
                boardValue += piece.value;
            }
            foreach (Piece piece in Board.blackTeam.alivePieces) {
                boardValue += piece.value;
            }

            value = boardValue;

            if (Board.turn == Colour.WHITE ) {
                if (boardValue > alpha) {
                    alpha = boardValue;
                }
            }
            else {
                if (boardValue < beta) {
                    beta = boardValue;
                }
            }
        }
        else {
            boardValue = 9999;
            if (Board.turn == Colour.WHITE) {
                boardValue *= -1;
            }

            value = boardValue;

            if (Board.turn == Colour.WHITE) {
                if (boardValue > alpha) {
                    alpha = boardValue;
                }
            }
            else {
                if (boardValue < beta) {
                    beta = boardValue;
                }
            }
        }
    }

    public void evaluateBranchValues() {
        int num;
        if (Board.turn == Colour.WHITE) {
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

        value = num;

        if (Board.turn == Colour.WHITE) {
            if (num > alpha) {
                alpha = num;
            }
        }
        else {
            if (num < beta) {
                beta = num;
            }
        }
    }
}
