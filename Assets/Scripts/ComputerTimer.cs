using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerTimer : MonoBehaviour {
    public static float PAUSE_TIMER_START;
    public static float pauseTimer;
    public static bool willPlay;

    void Start() {
        PAUSE_TIMER_START = 1;
        pauseTimer = PAUSE_TIMER_START;
    }

    void Update() {
        if (willPlay) {
            if (pauseTimer <= 0) {
                pauseTimer = PAUSE_TIMER_START;
                willPlay = false;
                Computer.move();
            }
            else {
                pauseTimer -= Time.deltaTime;
            }
        }
    }
}
