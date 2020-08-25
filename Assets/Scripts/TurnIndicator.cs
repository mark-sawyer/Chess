using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnIndicator : MonoBehaviour {
    public Sprite white;
    public Sprite black;

    private void Start() {
        GameEvents.changeTurn.AddListener(changeSprite);
    }

    private void changeSprite() {
        if (GetComponent<SpriteRenderer>().sprite == white) {
            GetComponent<SpriteRenderer>().sprite = black;
        }
        else {
            GetComponent<SpriteRenderer>().sprite = white;
        }
    }
}
