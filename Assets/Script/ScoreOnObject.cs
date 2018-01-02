using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreOnObject : MonoBehaviour {
    public int score = 0;

    public int getScore()
    {
        return this.score; 
    }

    public void setScore(int _score)
    {
        this.score = _score;
    }
}
