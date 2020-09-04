using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressManager : MonoBehaviour {
    public LayerMask buttonLayer;
    public GameObject whiteWarning;
    public GameObject blackWarning;

    public void Start() {
        whiteWarning = GameObject.Find("white warning");
        blackWarning = GameObject.Find("black warning");

        GameObject currentButton;

        // Start button
        currentButton = GameObject.Find("start button");
        currentButton.GetComponent<Compressable>().button = new StartButton(currentButton);

        // Quit button
        GameObject.Find("quit button").GetComponent<Compressable>().button = new QuitButton();

        // Human button
        currentButton = GameObject.Find("white human button");
        currentButton.GetComponent<Compressable>().button = new HumanButton(Colour.WHITE, currentButton);
        currentButton = GameObject.Find("black human button");
        currentButton.GetComponent<Compressable>().button = new HumanButton(Colour.BLACK, currentButton);
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector2 mousePos = Input.mousePosition;
            RaycastHit2D ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePos), Vector2.zero, 0, buttonLayer);

            if (ray.collider != null && ray.collider.GetComponent<Compressable>().enabled) {
                ray.collider.GetComponent<Compressable>().compress();
            }
        }
    }

    public GameObject getWarningFromColour(Colour colour) {
        if (colour == Colour.WHITE) {
            return whiteWarning;
        }
        else {
            return blackWarning;
        }
    }
}
