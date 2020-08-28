using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPiece : MonoBehaviour {
    public LayerMask reachableLayer;
    public Space[,] board;
    private bool beingHeld;
    public Piece piece;
    public GameObject reachableSpace;

    private void Start() {
        board = Board.board;
        GameEvents.changeTurn.AddListener(removePiece);
    }

    private void Update() {
        if (beingHeld) {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetMouseButtonUp(0)) {
                RaycastHit2D ray = Physics2D.Raycast(mousePos, Vector2.zero, 0, reachableLayer);
                if (ray.collider != null) {
                    int newFile = ray.collider.GetComponent<ReachableSpace>().file;
                    int newRank = ray.collider.GetComponent<ReachableSpace>().rank;
                    piece.getMoveMatchingToSpace(board[newFile, newRank]).executeMove();
                    GameObject.Find("chess manager").GetComponent<ChessDisplayManager>().updateBoardDisplay();
                }
                else {
                    transform.position = new Vector3(piece.space.file, piece.space.rank, 0);
                    beingHeld = false;
                }
                GameEvents.removeReachableGameObjects.Invoke();
            }
            else {
                transform.position = new Vector3(mousePos.x, mousePos.y, -1);
            }
        }
    }

    public void startBeingHeld() {
        beingHeld = true;
        for (int i = 0; i < piece.playableMoves.Count; i++) {
            int file = piece.playableMoves[i].newSpace.file;
            int rank = piece.playableMoves[i].newSpace.rank;

            Instantiate(reachableSpace, new Vector3(file, rank, 0), Quaternion.identity);
        }
    }

    public void removePiece() {
        Destroy(gameObject);
    }
}
