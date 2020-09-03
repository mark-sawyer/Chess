using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressManager : MonoBehaviour {
    public LayerMask buttonLayer;
    public int whiteDifficulty;
    public int blackDifficulty;

    public void Start() {
        GameObject.Find("start button").GetComponent<Compressable>().button = new StartButton();
        GameObject.Find("human button").GetComponent<Compressable>().button = new HumanButton(Colour.WHITE, GameObject.Find("human button"));
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector2 mousePos = Input.mousePosition;
            RaycastHit2D ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePos), Vector2.zero, buttonLayer);
            
            if (ray.collider != null) {
                ray.collider.GetComponent<Compressable>().compress();
            }
        }
    }
}
