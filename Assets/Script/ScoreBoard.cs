using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreBoard : MonoBehaviour {

    public Text hudScourText;
    public Text highScore;
    public Text yourScore;
    public Button restartBtn;
    public Button quiteBtn;
    // Use this for initialization
    private void Awake()
    {
        if (restartBtn != null)
        {
            restartBtn.onClick.AddListener(delegate { restartGame(); });
        }
        if (quiteBtn != null)
        {
            quiteBtn.onClick.AddListener(delegate { quiteGame(); });
        }
        hideMe();
    }

    public void showMe()
    {
        gameObject.SetActive(true);
    }
    public void hideMe()
    {
        gameObject.SetActive(false);
    }

    public void setResultScore(int score)
    {
        yourScore.text = " Your Score \n" + score.ToString();
    }
    public void setHighScore(string score)
    {
        highScore.text =  score;
    }
    void restartGame()
    {
        print("restartGame");
        SceneManager.LoadScene("Level0");
    }
    void quiteGame()
    {
        Application.Quit();
    }
}
