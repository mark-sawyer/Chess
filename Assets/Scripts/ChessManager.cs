using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessManager : MonoBehaviour {
    public LayerMask pieceLayer;

    public static GameObject pawn;

    private void Awake() {
        pawn = Resources.Load<GameObject>("pawn");
    }

    void Start() {
        changeBoardDisplay();
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit2D ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, pieceLayer);
            if (ray.collider != null) {
                ray.collider.GetComponent<GameObjectPiece>().startBeingHeld();
            }
        }
    }

    public void changeBoardDisplay() {
        GameEvents.removePieces.Invoke();

        GameObject pieceBeingDisplayed;
        for (int file = 0; file < 8; file++) {
            for (int rank = 0; rank < 8; rank++) {
                if (!Board.board[file, rank].isEmpty) {
                    pieceBeingDisplayed = Instantiate(pawn, new Vector3(file, rank, 0), Quaternion.identity);
                    pieceBeingDisplayed.GetComponent<GameObjectPiece>().piece = Board.board[file, rank].piece;
                }
            }
        }
    }
}
