using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour {
    int health;
    Kill kill;
    BeeCtrl beeCtrl;
    dropItem dropItem;
	// Use this for initialization
	void Start () {
        kill = GetComponent<Kill>();
        beeCtrl = GetComponent<BeeCtrl>();
        dropItem = GetComponent<dropItem>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
        }
    }


    public void damage(int _damage)
    {
        if (beeCtrl!=null)
        {
            if (beeCtrl.canBeDamaged)
            {
                health -= _damage;
                damageVisual(_damage);
                if (health <= 0)
                {
                    if (kill != null)
                    {
                        kill.kill(true);
                    }

                    if (dropItem != null)
                    {
                        dropItem.drop();
                    }
                }
            }
           
        }
        
    }

    void damageVisual(int _damage = 1)
    {
        //bee
        if (beeCtrl!=null)
        {
            beeCtrl.damageVisual(_damage);
        }

    }
}
