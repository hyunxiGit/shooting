using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockControl : MonoBehaviour {

    public int Score = 3;
    public float speedScale = 1f;
    Rigidbody2D rgBody;
    float speed = 0.002f;
    float xForce;
    float yForce;

    bool move = false;
    private void Awake()
    {
        
        rgBody = gameObject.GetComponent<Rigidbody2D>();
        
    }

    private void Start()
    {
        speedScale = Random.Range(0.05f, 2f / transform.localScale.x) ;
        xForce = Random.Range(-0.0002f * speedScale, 0.0002f * speedScale);
        yForce = Random.Range(-0.001f * speedScale, -0.0003f * speedScale);
        rgBody.velocity = (new Vector2(xForce, yForce)*500f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerControl PlayerCtrl = collision.gameObject.GetComponent<PlayerControl>();

            if (PlayerCtrl != null)
            {
                PlayerCtrl.damage();
            }
        }
    }


}
