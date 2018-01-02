using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreLogic : MonoBehaviour {

    public GameObject keepScore;
    ScoreKeeper scoreKeeper;
    GameObject myScoreKeeper;
    public GameObject rockGenerator;
    EnemyGenerator rockGen;
    public int totalScaore = 0;
    public Canvas scoreBoard;
    ScoreBoard scoreBoardCtrl;
    public Canvas hudCanvas;
    Hud hudCtrl;
    public Canvas joyStick;
    JoyStick joyStickCtrl;

    private void Awake()
    {
        if (joyStick != null)
        {
            joyStickCtrl = joyStick.GetComponent<JoyStick>();
        }
        if (rockGenerator != null)
        {
            rockGen = rockGenerator.GetComponent<EnemyGenerator>();
        }
        if (scoreBoard!=null)
        {
            scoreBoardCtrl = scoreBoard.GetComponent<ScoreBoard>();
        }
        if (hudCanvas != null)
        {
            hudCtrl = hudCanvas.GetComponent<Hud>();
        }
        if (keepScore != null)
        {
            scoreKeeper = keepScore.GetComponent<ScoreKeeper>();
        }
    }

    private void Start()
    {
        myScoreKeeper = GameObject.Find("ScoreKeeper");
        if (myScoreKeeper == null)
        {
            myScoreKeeper = Instantiate(keepScore, Vector3.zero, transform.rotation);
            myScoreKeeper.name = "ScoreKeeper";
        }       
    }

    public void gainScore( GameObject gb)
    {
        ScoreOnObject targetScoreCtrl;
        if (gb!=null)
        {
            targetScoreCtrl = gb.GetComponent<ScoreOnObject>();
            if (targetScoreCtrl !=null)
            {
                int addScore = targetScoreCtrl.getScore();
                totalScaore = totalScaore + addScore;
                setHudScore();
            }
        }

    }
    public void gameOver()
    {
        rockGen.setOn(false);
       if (scoreBoardCtrl != null)
        {
            ScoreKeeper.Instance.setHighScore(totalScaore);
            scoreBoardCtrl.showMe();
            scoreBoardCtrl.setResultScore(totalScaore);
            scoreBoardCtrl.setHighScore(ScoreKeeper.Instance.getHighScore());
        }
       if (joyStickCtrl!=null)
        {
            joyStickCtrl.hideMe();
        }
    }

    void setHudScore ()
    {
        if (hudCtrl != null)
        {
            hudCtrl.setHudScore(totalScaore);
        }
    }
}
