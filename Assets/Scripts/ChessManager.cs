﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessManager : MonoBehaviour {
    void Start() {
        
    }

    void Update() {
        if (Input.GetKeyDown("space")) {
            Board.changeBoardDisplay();
        }
    }
}
