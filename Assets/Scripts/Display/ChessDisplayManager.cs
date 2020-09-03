using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessDisplayManager : MonoBehaviour {
    public LayerMask pieceLayer;
    public Space[,] board;

    public void start() {
        board = Board.board;
    }

    void Update() {
        if (Input.GetMouseButtonDown(0) && ((Board.turn == Colour.WHITE && !Board.whiteIsAI) || (Board.turn == Colour.BLACK && !Board.blackIsAI))) {
            RaycastHit2D ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 0, pieceLayer);
            if (ray.collider != null && ray.collider.GetComponent<GameObjectPiece>().piece.colour == Board.turn) {
                ray.collider.GetComponent<GameObjectPiece>().startBeingHeld();
            }
        }
    }

    public void updateBoardDisplay() {
        GameObject pieceBeingDisplayed;
        for (int file = 0; file < 8; file++) {
            for (int rank = 0; rank < 8; rank++) {
                if (!board[file, rank].isEmpty) {
                    Piece piece = board[file, rank].piece;
                    pieceBeingDisplayed = Instantiate(piece.getGameObject(), new Vector3(file, rank, 0), Quaternion.identity);
                    pieceBeingDisplayed.GetComponent<GameObjectPiece>().piece = board[file, rank].piece;
                }
            }
        }
    }
}