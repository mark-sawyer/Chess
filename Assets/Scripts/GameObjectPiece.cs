using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPiece : MonoBehaviour {
    private bool beingHeld;
    public Piece piece;

    private void Start() {
        GameEvents.removePieces.AddListener(removePiece);
    }

    private void Update() {
        if (beingHeld) {
            if (Input.GetMouseButtonUp(0)) {
                transform.position = new Vector3(piece.space.file, piece.space.rank, 0);
                beingHeld = false;
            }
            else {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = new Vector3(mousePos.x, mousePos.y, -1);
                print("file: " + piece.space.file + " rank: " + piece.space.rank);
            }
        }
    }

    public void startBeingHeld() {
        beingHeld = true;
    }

    public void removePiece() {
        Destroy(gameObject);
    }
}
