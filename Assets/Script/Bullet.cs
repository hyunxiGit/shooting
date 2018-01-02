using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed = 10f;
    ScoreLogic scoreLogic;
    Rigidbody2D rgBody;
    Kill kill;

    private void Awake()
    {
        rgBody = GetComponent<Rigidbody2D>();
        kill = GetComponent<Kill>();
    }
    private void FixedUpdate()
    {
        rgBody.velocity = new Vector2(0f, speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Damage damage = collision.gameObject.GetComponent<Damage>();
            if (damage !=null)
            {
                damage.damage(10);
            }
            // kill projectile
            if (kill != null)
            {
                kill.kill();
            }
        }
        if (collision.gameObject.tag == "Destroyable")
        {
            
            //score
            if (scoreLogic != null)
            {
                scoreLogic.gainScore(collision.gameObject);
            }
            //kill Rock
            Kill killCtrl = collision.gameObject.GetComponent<Kill>();
            if (killCtrl != null)
            {
                killCtrl.kill();
            }
            // kill projectile
            if (kill != null)
            {
                kill.kill();
            }

        }

    }

    public void setScoreLoc(ScoreLogic logoc)
    {
        scoreLogic = logoc;
    }
}
