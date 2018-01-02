using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeProjectile : MonoBehaviour {
    GameObject center;
    Rigidbody2D rgBody;

    float maxLifeTime = 15f;
    float lifeTime;

    Vector2 direction;
    // Use this for initialization
    private void Awake()
    {
        rgBody = GetComponent<Rigidbody2D>();
    }
    void Start () {
       
    }
	
    private void FixedUpdate()
    {
        direction = transform.position - center.transform.position;
        rgBody.velocity = direction * 1f;

    }
    private void OnTriggerEnter2D(Collider2D collision)
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

    public void setCenter(GameObject _center)
    {
        
        this.center = _center;
    }

}
