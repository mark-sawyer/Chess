using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Button {
    void performAction();
}

public class StartButton : Button {
    GameObject visual;

    public StartButton(GameObject visual) {
        this.visual = visual;
    }

    public void performAction() {
        GameEvents.resetForNewGame.Invoke();
        Board.startGame();

        GameObject endGameButton = Object.Instantiate(Resources.Load<GameObject>("Button/end game button"), visual.transform.position, Quaternion.identity);
        endGameButton.GetComponent<Compressable>().button = new EndGameButton(endGameButton);
        GameEvents.changeCompressability.Invoke(false);
        Object.Destroy(visual);
    }
}

public class EndGameButton : Button {
    GameObject visual;

    public EndGameButton(GameObject visual) {
        this.visual = visual;
    }

    public void performAction() {
        Board.endGame();

        GameObject startButton = Object.Instantiate(Resources.Load<GameObject>("Button/start button"), visual.transform.position, Quaternion.identity);
        startButton.GetComponent<Compressable>().button = new StartButton(startButton);
        GameEvents.changeCompressability.Invoke(true);
        Object.Destroy(visual);
    }
}

public class HumanButton : Button {
    Colour colour;
    GameObject visual;

    public HumanButton(Colour colour, GameObject visual) {
        this.colour = colour;
        this.visual = visual;

        GameEvents.changeCompressability.AddListener(turnOff);
    }

    public void performAction() {
        if (Board.getComputerFromColour(colour).maxLevel == 4) {
            GameObject.Find("chess manager").GetComponent<ButtonPressManager>().getWarningFromColour(colour).GetComponent<SpriteRenderer>().enabled = true;
        }

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

    private void turnOff(bool b) {
        if (visual != null) {
            visual.GetComponent<Compressable>().enabled = b;
        }
    }
}

public class AIButton : Button {
    Colour colour;
    GameObject visual;

    public AIButton(Colour colour, GameObject visual) {
        this.colour = colour;
        this.visual = visual;

        GameEvents.changeCompressability.AddListener(turnOff);
    }

    public void performAction() {
        GameObject.Find("chess manager").GetComponent<ButtonPressManager>().getWarningFromColour(colour).GetComponent<SpriteRenderer>().enabled = false;

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

    private void turnOff(bool b) {
        if (visual != null) {
            visual.GetComponent<Compressable>().enabled = b;
        }
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
            visual.GetComponent<Compressable>().enabled = false;
        }

        GameEvents.changeCompressability.AddListener(turnOff);
    }

    public void performAction() {
        GameObject otherDifficulty;
        for (int i = 0; i < 4; i++) {
            if (i != difficulty - 1) {
                otherDifficulty = visual.transform.parent.GetChild(i).gameObject;
                otherDifficulty.GetComponent<SpriteRenderer>().sprite = otherDifficulty.GetComponent<Compressable>().regularSprite;
                otherDifficulty.GetComponent<Compressable>().enabled = true;
            }
        }

        if (difficulty == 4) {
            GameObject.Find("chess manager").GetComponent<ButtonPressManager>().getWarningFromColour(colour).GetComponent<SpriteRenderer>().enabled = true;
        }
        else {
            GameObject.Find("chess manager").GetComponent<ButtonPressManager>().getWarningFromColour(colour).GetComponent<SpriteRenderer>().enabled = false;
        }
                        
        computer.maxLevel = difficulty;
        visual.GetComponent<Compressable>().enabled = false;
        visual.GetComponent<SpriteRenderer>().sprite = visual.GetComponent<Compressable>().compressedSprite;
    }

    private void turnOff(bool b) {
        if (visual != null) {
            visual.GetComponent<Compressable>().enabled = b;
        }
    }
}

public class QuitButton : Button {
    public void performAction() {
        Application.Quit();
    }
}