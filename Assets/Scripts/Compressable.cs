using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compressable : MonoBehaviour {
    private bool compressed;
    private bool overButton;

    public Sprite compressedSprite;
    public Sprite regularSprite;
    private Sprite currentSprite;

    public Button button;

    private void Start() {
        currentSprite = regularSprite;
    }

    private void Update() {
        if (compressed) {
            Vector2 mousePos = Input.mousePosition;
            RaycastHit2D ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePos), Vector2.zero);

            if (ray.collider == GetComponent<BoxCollider2D>()) {
                overButton = true;
            }
            else {
                overButton = false;
            }

            if ((overButton && currentSprite != compressedSprite) || (!overButton && currentSprite == compressedSprite)) {
                changeSprite();
            }

            if (Input.GetMouseButtonUp(0)) {
                compressed = false;
                if (overButton) {
                    currentSprite = regularSprite;
                    GetComponent<SpriteRenderer>().sprite = regularSprite;
                    button.performAction();
                }
            }
        }
    }

    public void compress() {
        compressed = true;
        currentSprite = compressedSprite;
        GetComponent<SpriteRenderer>().sprite = compressedSprite;
    }

    private void changeSprite() {
        if (currentSprite == regularSprite) {
            currentSprite = compressedSprite;
            GetComponent<SpriteRenderer>().sprite = compressedSprite;
        }
        else {
            currentSprite = regularSprite;
            GetComponent<SpriteRenderer>().sprite = regularSprite;
        }
    }
}
