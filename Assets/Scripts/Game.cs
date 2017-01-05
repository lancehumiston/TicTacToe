using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class Game : MonoBehaviour {

    enum Player {
        Player1,
        Player2
    }

    List<Button> buttons;
    bool isPlayerOnesTurn;
    List<WinSequence> winningCombos;
    Dictionary<Player, List<int>> playerTurns;
    Dictionary<Player, int> scores;
    bool isGameOver = false;
    
    void Start () {
        initalizeGameBoard();
        createWinningSequences();
        restartGame();

        foreach (Button button in buttons) {
            string buttonName = button.name;
            button.onClick.AddListener(() => {
                bool isValidMove = placePiece(buttonName);
                if (isValidMove)
                {
                    isGameOver = checkForWinner();
                    if (isGameOver) {
                        updateScore();
                    } else {
                        isPlayerOnesTurn = !isPlayerOnesTurn;
                    }
                }
            });
        }
    }

    void initalizeGameBoard() {
        buttons = new List<Button>() {
            GameObject.Find("topLeft").GetComponent<Button>(),
            GameObject.Find("topMiddle").GetComponent<Button>(),
            GameObject.Find("topRight").GetComponent<Button>(),
            GameObject.Find("midLeft").GetComponent<Button>(),
            GameObject.Find("midMiddle").GetComponent<Button>(),
            GameObject.Find("midRight").GetComponent<Button>(),
            GameObject.Find("bottomLeft").GetComponent<Button>(),
            GameObject.Find("bottomMiddle").GetComponent<Button>(),
            GameObject.Find("bottomRight").GetComponent<Button>()
        };
    }

    void createWinningSequences() {
        winningCombos = new List<WinSequence>();
        winningCombos.Add(new WinSequence(0, 1, 2));
        winningCombos.Add(new WinSequence(3, 4, 5));
        winningCombos.Add(new WinSequence(6, 7, 8));
        winningCombos.Add(new WinSequence(0, 3, 6));
        winningCombos.Add(new WinSequence(1, 4, 7));
        winningCombos.Add(new WinSequence(2, 5, 8));
        winningCombos.Add(new WinSequence(0, 4, 8));
        winningCombos.Add(new WinSequence(2, 4, 6));
    }

    void Update()
    {
        if (isGameOver) {
            if (Input.GetKeyDown("space")) {
                restartGame();
            }
        }
    }

    void updateScore() {
        if (isPlayerOnesTurn) {
            scores[Player.Player1]++;
        } else {
            scores[Player.Player2]++;
        }
        GameObject.Find("ScoreBoard").GetComponent<Text>().text = "Player1: " +
                                                            scores[Player.Player1].ToString() +
                                                            "\nPlayer2: " + scores[Player.Player2].ToString();
    }

    void restartGame()
    {
        if (null == scores) {
            scores = new Dictionary<Player, int>();
            scores.Add(Player.Player1, 0);
            scores.Add(Player.Player2, 0);
        }

        playerTurns = new Dictionary<Player, List<int>>();
        playerTurns.Add(Player.Player1, new List<int>());
        playerTurns.Add(Player.Player2, new List<int>());

        isPlayerOnesTurn = true;
        isGameOver = false;
        foreach (Button button in buttons) {
            button.GetComponentInChildren<Text>().text = "";
        }
        GameObject.Find("Title").GetComponent<Text>().text = "TicTacToe";
        GameObject.Find("Instructions").GetComponent<Text>().text = "Player 1 Turn";
    }

    bool placePiece(string buttonName) {
        Player player = (isPlayerOnesTurn) ? Player.Player1 : Player.Player2;
        Button button = GameObject.Find(buttonName).GetComponent<Button>();
        if (button.GetComponentInChildren<Text>().text.Length <= 0)
        {
            if (isPlayerOnesTurn)
            {
                button.GetComponentInChildren<Text>().text = "X";
            }
            else
            {
                button.GetComponentInChildren<Text>().text = "O";
            }
            switch (buttonName) {
                case "topLeft":
                    playerTurns[player].Add(0);
                    break;
                case "topMiddle":
                    playerTurns[player].Add(1);
                    break;
                case "topRight": 
                    playerTurns[player].Add(2);
                    break;
                case "midLeft": 
                    playerTurns[player].Add(3);
                    break;
                case "midMiddle": 
                    playerTurns[player].Add(4);
                    break;
                case "midRight": 
                    playerTurns[player].Add(5);
                    break;
                case "bottomLeft": 
                    playerTurns[player].Add(6);
                    break;
                case "bottomMiddle":
                    playerTurns[player].Add(7);
                    break;
                case "bottomRight":
                    playerTurns[player].Add(8);
                    break;
            }
            playerTurns[player].Sort();
            return true;
        }
        else {
            return false;
        }
        
    }

    bool checkForWinner() {
        Player lastPlayer = (isPlayerOnesTurn) ? Player.Player1 : Player.Player2;

        foreach (var winningCombo in winningCombos) {
            if (winningCombo.combo.All(idx => playerTurns[lastPlayer].Contains(idx))) {
                GameObject.Find("Title").GetComponent<Text>().text = lastPlayer.ToString() + " WINS!!";
                GameObject.Find("Instructions").GetComponent<Text>().text = "Press 'SPACE'";
                return true;
            }
        }

        if (playerTurns[Player.Player1].Count + playerTurns[Player.Player2].Count >= 9)
        {
            GameObject.Find("Title").GetComponent<Text>().text = "It's a tie!!";
            GameObject.Find("Instructions").GetComponent<Text>().text = "Press 'SPACE'";
        }
        else
        {
            GameObject.Find("Instructions").GetComponent<Text>().text = lastPlayer.ToString() + "Turn";
        }
        return false;
    }
}
