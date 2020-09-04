using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfGameMessage : MonoBehaviour {
    public Sprite checkmate;
    public Sprite stalemate;
    public Sprite fiftyMove;

    void Start() {
        
    }

    void Update() {
        
    }

    public void turnOnMessage() {
        if (Board.gameIsOver) {
            GetComponent<SpriteRenderer>().sprite = checkmate;
        }
        else if (Board.gameIsStalemate) {
            GetComponent<SpriteRenderer>().sprite = stalemate;
        }
        else {
            GetComponent<SpriteRenderer>().sprite = fiftyMove;
        }

        GetComponent<SpriteRenderer>().enabled = true;
    }
}
