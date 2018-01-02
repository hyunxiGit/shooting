using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour {
    PlayerControl playerCtl;

    float flickStartTime;
    SpriteRenderer sprite;
    int flickTime = 0;
    Kill kill;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        kill = GetComponent<Kill>();
    }
    // Use this for initialization
    void Start()
    {
        flickStartTime = 0;


    }

    // Update is called once per frame
    void Update()
    {
        flick();
    }

    void flick()
    {
        if (Time.time > flickStartTime + 1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);
            flickStartTime = Time.time;
            flickTime++;

            if (flickTime >= 3)
            {
                flickTime = 0;
            }
        }
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, Mathf.Clamp(sprite.color.a - 0.1f, 0.3f, 1f));
        float scale = Mathf.Clamp(transform.localScale.x - 0.1f, 0.8f, 1f);
        transform.localScale = new Vector3(scale, scale, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            playerCtl = collision.gameObject.GetComponent<PlayerControl>();
            
            if (playerCtl != null)
            {
                print("here");
                playerCtl.addLife(1);

                if(kill!=null)
                {
                    kill.kill();
                }

            }
        }
    }

}
