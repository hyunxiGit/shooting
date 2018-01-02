using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour {
    public static ScoreKeeper Instance;
    // Use this for initialization
    static int highScore = 0;
    static List<int> highScores = new List<int>();

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);

        highScores.Add(90);
        highScores.Add(5);
        highScores.Add(0);


    }
    
    public void setHighScore(int score)
    {

        print(highScores[0]);
        print(highScores[1]);
        print(highScores[2]);

        int index = -1;
        for (int i = 0; i < 3; i++)
        {
            if (score > highScores[i])
            {
                index = i;
                break;
            }
        }

        if (index != -1)
        {
            highScores.Insert(index, score);
            highScores.RemoveAt(3);
        }

        if (score > highScore)
            highScore = score;
    }

    public string getHighScore ()
    {

        return highScores[0].ToString()+"\n " + highScores[1].ToString() + "\n " + highScores[2].ToString() + " ";
    }
}
