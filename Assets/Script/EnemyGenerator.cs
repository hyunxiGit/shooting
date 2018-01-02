using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour {

    public GameObject player;
    //Bee
    public GameObject bee;
    BeeCtrl beeCtrl;
    float beeReleaseTime = 10;
    float beeReleaseTimeGap = 30;

    //rock
    public GameObject rock;
    public float rockRate = 1.5f;
    float rockMinX = -3f;
    float rockMaxX = 3f;

    float rockCurX;
    float rockCurY = 6.8f;

    float rockMinSize = 0.2f;
    float rockMaxSize = 1f;

    float releaseRockTime = 0;

    bool on = true;

    private void Awake()
    {
        if (bee!=null)
        {
            beeCtrl = bee.GetComponent<BeeCtrl>();
        }
    }

    // Update is called once per frame
    void Update ()
    {
        rockMove();
        ReleaseBee();
    }

    void rockMove()
    {
        if (!on)
            return;
        if (releaseRockTime < Time.time)
        {
            releaseRock();
            releaseRockTime = Time.time + Random.Range(1f, rockRate);
        }
    }

    void positioning()
    {
        rockCurX = Random.Range(rockMinX, rockMaxX);;
    }

    void releaseRock()
    {
        positioning();
        GameObject myRock = Instantiate(rock, new Vector2(rockCurX, rockCurY), transform.rotation, transform);
        myRock.transform.localScale = new Vector3 (1f,1f,1f)*Random.Range(rockMinSize, rockMaxSize);
    }

    public void setOn(bool on)
    {
        this.on = on;
    }
    
    void ReleaseBee()
    {
        if (Time.time > beeReleaseTime )
        {
            if (bee!=null)
            {
                GameObject targetBee = Instantiate(bee, new Vector2(0, beeCtrl.startY), transform.rotation, transform);
                targetBee.GetComponent<BeeCtrl>().player = this.player;
                beeReleaseTime = Time.time + beeReleaseTimeGap;
            }     
        }
    }
}
