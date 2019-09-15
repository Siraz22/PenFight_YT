using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool StartGame = false;

    public static GameManager GMStaticInstance;
    public int CurrentPenID = 0; //By default pen 1 is always selected

    public TextMeshProUGUI Player1ScoreTEXT;
    public TextMeshProUGUI Player2ScoreTEXT;

    public float TimeLeft = 120f;
    public TextMeshProUGUI TimerTEXT;

    public TextMeshProUGUI WinnerTEXT;

    public GameObject GameendsPanel;
    public GameObject RestartButton;

    public int Player1Score;
    public int Player2Score;

    public void RestartButtonFunction()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Start is called before the first frame update
    void Start()
    {
        GMStaticInstance = this;
    }

    public void GameEnds()
    {
        if(Player1Score>Player2Score)
        {
            WinnerTEXT.text = "Player1 Wins! Restart?";
            WinnerTEXT.color = new Color32(0,125,255,255);
        }
        else if(Player2Score>Player1Score)
        {
            WinnerTEXT.text = "Player2 Wins! Restart?";
            WinnerTEXT.color = new Color32(0, 255, 28, 255);
        }
        else
        {
            WinnerTEXT.text = "It's a draw! Try Again?";
            WinnerTEXT.color = new Color32(255, 255, 255, 255);
        }

        GameendsPanel.gameObject.SetActive(true);
        RestartButton.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //Keep updating the scores in real time. Quick fix for a prototyoe but performance heavy

        Player1ScoreTEXT.text = "Score :" + Player1Score.ToString();
        Player2ScoreTEXT.text = "Score :"+ Player2Score.ToString();    

        if(StartGame)
        {
            if (TimeLeft <= 0)
            {
                GameEnds();
            }
            else
            {
                TimeLeft -= Time.deltaTime;
                TimerTEXT.text = "Time Left : " + TimeLeft.ToString("0") + "s";
            }
        }
    }

    public void StartTheGameButtonFunction()
    {
        StartGame = true;
    }
}
