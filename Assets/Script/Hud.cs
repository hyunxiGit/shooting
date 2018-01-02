using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour {

    public Text scourText;
    public Text lifeText;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setHudScore(int score)
    {
        scourText.text = " Score  " + score.ToString();
    }

    public void setHudLife(int life)
    {
        lifeText.text = "x " + life.ToString();
    }

    public void showMe()
    {
        gameObject.SetActive(true);
    }
    public void hideMe()
    {
        gameObject.SetActive(false);
    }
}
