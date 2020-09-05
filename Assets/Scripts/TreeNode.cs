using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeNode {
    public int level;
    public float value;
    public Move move;
    public List<TreeNode> branchingNodes;
    public float alpha;
    public float beta;

    public TreeNode(int level) {
        this.level = level;
        branchingNodes = new List<TreeNode>();
    }

    public TreeNode(int level, Move move, int value) {
        this.level = level;
        this.move = move;
        this.value = value;
        branchingNodes = new List<TreeNode>();
    }

    public void getBranchingNodes() {
        int totalPlayableMoves = 0;
        Team playingTeam;

        if (Board.turn == Colour.WHITE) {
            playingTeam = Board.whiteTeam;

            // Find the appropriate capacity
            foreach (Piece piece in playingTeam.alivePieces) {
                totalPlayableMoves += piece.playableMoves.Count;
            }
            branchingNodes = new List<TreeNode>(totalPlayableMoves);

            foreach (Piece piece in playingTeam.alivePieces) {
                foreach (Move move in piece.playableMoves) {
                    branchingNodes.Add(new TreeNode(level + 1, move, Computer.WORST_WHITE_SCORE));                    
                }
            }
        }
        else {
            playingTeam = Board.blackTeam;

            // Find the appropriate capacity
            foreach (Piece piece in playingTeam.alivePieces) {
                totalPlayableMoves += piece.playableMoves.Count;
            }
            branchingNodes = new List<TreeNode>(totalPlayableMoves);

            foreach (Piece piece in playingTeam.alivePieces) {
                foreach (Move move in piece.playableMoves) {
                    branchingNodes.Add(new TreeNode(level + 1, move, Computer.WORST_BLACK_SCORE));                    
                }
            }
        }
    }

    public void evaluateBoardValue() {
        if (Board.gameIsOver) {
            if (Board.turn == Colour.WHITE) {
                value = Computer.WORST_WHITE_SCORE;
            }
            else {
                value = Computer.WORST_BLACK_SCORE;
            }

            alphaBetaPruning();
        }
        else if (Board.gameIsStalemate) {
            value = 0;

            alphaBetaPruning();
        }
        else {
            float boardValue = 0;
            foreach (Piece piece in Board.whiteTeam.alivePieces) {
                if (piece is Pawn && ((Pawn)piece).isPromoted) {
                    boardValue += ((Pawn)piece).promotionQueen.value;
                }
                else {
                    boardValue += piece.value;
                }
            }
            foreach (Piece piece in Board.blackTeam.alivePieces) {
                if (piece is Pawn && ((Pawn)piece).isPromoted) {
                    boardValue += ((Pawn)piece).promotionQueen.value;
                }
                else {
                    boardValue += piece.value;
                }
            }

            value = boardValue;

            alphaBetaPruning();
        }
    }

    public void evaluateBranchValues() {
        float num;
        if (Board.turn == Colour.WHITE) {
            num = Computer.WORST_WHITE_SCORE;
            foreach (TreeNode branchNode in branchingNodes) {
                if (branchNode.value > num) {
                    num = branchNode.value;
                }
            }
        }
        else {
            num = Computer.WORST_BLACK_SCORE;
            foreach (TreeNode branchNode in branchingNodes) {
                if (branchNode.value < num) {
                    num = branchNode.value;
                }
            }
        }

        value = num;
    }

    public void alphaBetaPruning() {
        if (Board.turn == Colour.WHITE) {
            if (value > alpha) {
                alpha = value;
            }
        }
        else {
            if (value < beta) {
                beta = value;
            }
        }
    }
}
