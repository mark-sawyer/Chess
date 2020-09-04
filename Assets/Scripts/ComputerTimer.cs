using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerTimer : MonoBehaviour {
    public static float PAUSE_TIMER_START;
    public static float pauseTimer;
    public static bool willPlay;

    void Start() {
        PAUSE_TIMER_START = 0.2f;
        pauseTimer = PAUSE_TIMER_START;
    }

    void Update() {
        if (willPlay) {
            if (pauseTimer <= 0) {
                pauseTimer = PAUSE_TIMER_START;
                willPlay = false;
                if (Board.turn == Colour.WHITE) {
                    Board.whiteComputer.move();
                }
                else {
                    Board.blackComputer.move();
                }
            }
            else {
                pauseTimer -= Time.deltaTime;
            }
        }
    }

    public static void abort() {
        willPlay = false;
        pauseTimer = PAUSE_TIMER_START;
    }
}
