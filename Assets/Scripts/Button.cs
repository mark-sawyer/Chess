using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Button {
    public abstract void performAction();
}

public class StartButton : Button {
    public override void performAction() {
        Board.resetBoard();
        GameObject.Find("chess manager").GetComponent<ChessDisplayManager>().updateBoardDisplay();
    }
}

public class HumanButton : Button {
    Colour colour;
    GameObject visual;

    public HumanButton(Colour colour, GameObject visual) {
        this.colour = colour;
        this.visual = visual;
    }

    public override void performAction() {
        if (colour == Colour.WHITE) {
            Board.whiteIsAI = true;
        }
        else {
            Board.blackIsAI = true;
        }

        GameObject aiLayout = Object.Instantiate(Resources.Load<GameObject>("Button/ai layout"), visual.transform.position, Quaternion.identity);
        GameObject aiButton = aiLayout.transform.GetChild(0).gameObject;
        aiButton.GetComponent<Compressable>().button = new AIButton(colour, aiButton);
        GameObject difficultyButton;
        for (int i = 0; i < 4; i++) {
            difficultyButton = aiLayout.transform.GetChild(1).GetChild(i).gameObject;
            difficultyButton.GetComponent<Compressable>().button = new DifficultyButton(i + 1, colour, aiLayout.transform.GetChild(1).GetChild(i).gameObject);
        }
        Object.Destroy(visual);
    }
}

public class AIButton : Button {
    Colour colour;
    GameObject visual;

    public AIButton(Colour colour, GameObject visual) {
        this.colour = colour;
        this.visual = visual;
    }

    public override void performAction() {
        if (colour == Colour.WHITE) {
            Board.whiteIsAI = false;
        }
        else {
            Board.blackIsAI = false;
        }

        GameObject aiLayout = visual.transform.parent.gameObject;
        GameObject humanButton = Object.Instantiate(Resources.Load<GameObject>("Button/human button"), aiLayout.transform.position, Quaternion.identity);
        humanButton.GetComponent<Compressable>().button = new HumanButton(colour, humanButton);
        Object.Destroy(aiLayout);
    }
}

public class DifficultyButton : Button {
    public int difficulty;
    public Colour colour;
    Computer computer;
    GameObject visual;

    public DifficultyButton(int difficulty, Colour colour, GameObject visual) {
        this.difficulty = difficulty;
        this.colour = colour;
        this.visual = visual;
        computer = Board.getComputerFromColour(colour);
        if (computer.maxLevel == difficulty) {
            visual.GetComponent<SpriteRenderer>().sprite = visual.GetComponent<Compressable>().compressedSprite;
        }
    }

    public override void performAction() {
            GameObject otherDifficulty;
            for (int i = 0; i < 4; i++) {
                if (i != difficulty - 1) {
                    otherDifficulty = visual.transform.parent.GetChild(i).gameObject;
                    otherDifficulty.GetComponent<SpriteRenderer>().sprite = otherDifficulty.GetComponent<Compressable>().regularSprite;
                    otherDifficulty.GetComponent<Compressable>().enabled = true;
                }
            }

            computer.maxLevel = difficulty;
            visual.GetComponent<Compressable>().enabled = false;
            visual.GetComponent<SpriteRenderer>().sprite = visual.GetComponent<Compressable>().compressedSprite;
        }
}