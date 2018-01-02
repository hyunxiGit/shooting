using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeCtrl : MonoBehaviour {
    public GameObject projectilePos;
    public GameObject projectiles;
    float projectTime = 0;
    float projectRate = 3;

    public GameObject player;

    public float startY = 6f;
    float minX = -1.88f;
    float maxX = 1.88f;

    float minY = -4.2f;
    float maxY = 4.2f;

    Rigidbody2D rgBody;
    // Use this for initialization
    Vector2 target;
    float targetDistance = 0f;

    enum BeeState { GENERATING,ALIVE, DEAD, DAMAGED };
    BeeState state;
    public bool canBeDamaged = false;
    bool isRushing = false;
    float rushTime = 0f;
    float rushSpeed = 6f;

    List<GameObject> block;
    int health;
    Damage damage;

    private void Awake()
    {
        rgBody = GetComponent<Rigidbody2D>();
        setMode(BeeState.GENERATING);
        block = new List<GameObject>();
        health = transform.childCount;
        for (int i = 0;i < health; i++)
        {
            GameObject targetGameObject = transform.GetChild(i).gameObject;
            if (targetGameObject.GetComponent<BeeBlock>()!=null)
            {
                block.Add(transform.GetChild(i).gameObject);
            }  
        }
        damage = GetComponent<Damage>();
        if (damage!=null)
        {
            damage.Health = health;
        }
        
    }

	
	// Update is called once per frame
	void Update ()
    {
        if (state == BeeState.GENERATING)
        {
            rgBody.velocity = new Vector2(0, -0.5f);
            if (transform.position.y< 4.15f)
            {
                setMode( BeeState.ALIVE);
                rushTime = Time.time + 3f;
            }
        }
        if (state == BeeState.ALIVE)
        {
            setRush();
            move();
            shoot();
        }
        
	}

    void move()
    {
        bool outRange = transform.position.x < minX || transform.position.x > maxX || transform.position.y < minY || transform.position.y > maxY;
        if (state == BeeState.ALIVE )
        {        
            if (targetDistance <= 0.01f || outRange )
            {

                if (isRushing && player != null)
                {
                    target = player.transform.position;
                    rgBody.velocity = (target-new Vector2(transform.position.x, transform.position.y)).normalized* rushSpeed;
                    isRushing = false;
                }
                else
                {
                    target = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                    rgBody.velocity = target.normalized;
                }  
            }
            targetDistance = Vector2.Distance(target, rgBody.position);
        }

        transform.position = new Vector2(Mathf.Clamp(transform.position.x, minX, maxX),
                                 Mathf.Clamp(transform.position.y, minY, maxY));

    }

    void setRush()
    {
        if (!isRushing)
        {
            if (rushTime < Time.time)
            {
                this.isRushing = true;
                rushTime = Time.time + 7f;
            }
        }
        
    }

   void setMode(BeeState _state)
   {
        this.state = _state;
        if (this.state == BeeState.ALIVE)
        {
            this.canBeDamaged = true;
        }
        else
        {
            this.canBeDamaged = false;
        }
    }

    public void damageVisual(int _damage)
    {
        //block set Color 
        int damage = _damage-1;
        while (damage >0&& block.Count>0)
        {
            int index = (int)Random.Range(0, block.Count);
            block[index].GetComponent<BeeBlock>().setdamage();
            block.RemoveAt(index);
            damage--;
        }
        
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

    void shoot()
    {
        if (Time.time > projectTime + projectRate)
        {
            if (projectiles !=null && projectilePos!=null)
            {
                Instantiate(projectiles, projectilePos.transform.position, projectilePos.transform.rotation);
                projectTime = Time.time;
            }
        }
    }

}
