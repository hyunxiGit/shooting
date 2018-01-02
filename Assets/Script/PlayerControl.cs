using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {
    public bool useJoyStick = false;
    public int life = 3;

    public GameObject propeller;
    public ScoreLogic scoreLogic;
    public GameObject shootPoint;
    public GameObject projectile;
    public Canvas joyStickLayer;
    public Canvas hudCanvas;
    Hud hudCtrl;

    //edge position
    float minX = -2.272f;
    float maxX = 2.348f;
    float minY = -4.408f;
    float maxY = 4.342f;

    //physics
    Rigidbody2D rgBody;
    BoxCollider2D collider;
    SpriteRenderer playerSprite;
    float shootTime = 0f;
    float shootRate = 0.5f;
    float speed = 5f;
    float xForce;
    float yForce;

    Vector2 startPosition;

   // bool alive = true;
    enum PlayerState { ALIVE, DEAD, DAMAGED,GOD};
    float godTime;
    PlayerState state; 
    JoyStick joyStick;

    //player lose life flick
    float flickStartTime;
    int flickTime = 0;

    float lastAddTime = 0f;
    float yScale = 0f;
    float propellerAddDirection = 1;

    private void Awake()
    {
        state = PlayerState.ALIVE;
        if (hudCanvas != null)
        {
            hudCtrl = hudCanvas.GetComponent<Hud>();
        }
        if (joyStickLayer!=null)
        {
            joyStick = joyStickLayer.GetComponent<JoyStick>();
        }
        rgBody = gameObject.GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
        propeller.transform.localScale = new Vector2(1f, 0f);
        
        if (hudCtrl != null)
        {
            hudCtrl.setHudLife(life);
        }
    }

    // Use this for initialization
    void Start ()
    {
        startPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        joyStickVisual();
        if(state == PlayerState.ALIVE)
        {
            shoot();
            move(); 
            propellerLogic();
        }
        else if (state == PlayerState.DAMAGED)
        {
            move();
            lifeLoseFlick();
        }
        else if (state == PlayerState.GOD)
        {
            shoot();
            move();
            propellerLogic();
            if (Time.time  > godTime + 3f)
            {
                setPlayerMode(PlayerState.ALIVE);
            }
        }
        else
        {

        }

    }

    private void FixedUpdate()
    {
        rgBody.velocity = new Vector2(xForce, yForce);
        checkEdge();
    }

    void joyStickVisual()
    {
        if (joyStick!=null)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                joyStick.showMe();
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                joyStick.hideMe();
            }
        }
    }

    void propellerLogic()
    {
        float minScale = 0;
        if(rgBody.velocity.y == 0)
        {
            lastAddTime = 0f;
            minScale = 0;
            propellerAddDirection = -2f;
        }
        else if (rgBody.velocity.y < 0)
        {
            minScale = 0.2f;
            propellerAddDirection = -2f;
        }
        else
        {
            minScale = 0.2f;
            propellerAddDirection = 1f;
        }

        if (Time.time > lastAddTime)
        {
            if (yScale<=2)
            {
                yScale = Mathf.Clamp(yScale + 0.1f* propellerAddDirection, minScale, 2f);
                lastAddTime = Time.time + 0.01f;
            }
        }

        propeller.transform.localScale = new Vector2(1f, yScale);
    }

    public void shoot()
    {
        //shoot rate 1s;
        if (shootPoint != null && projectile != null)
        {
            if (shootTime == 0f)
            {
                shootTime = Time.time;
            }

            if (shootTime < Time.time)
            {
                GameObject rocket = Instantiate(projectile, shootPoint.transform.position, projectile.transform.rotation, transform);
                rocket.GetComponent<Bullet>().setScoreLoc(scoreLogic);
                shootTime = Time.time + shootRate;
            }
        }
    }

    void move()
    {
        if (useJoyStick)
        {
            xForce = joyStick.getJoyStickVector().x * speed;
            yForce = joyStick.getJoyStickVector().y * speed;
        }
        else
        {
            xForce = Input.GetAxis("Horizontal") * speed;
            yForce = Input.GetAxis("Vertical") * speed;
        }

    }

    void lifeLoseFlick()
    {
        
        propeller.transform.localScale = new Vector2(1f, 0f);
        if (Time.time > flickStartTime + 1f)
        {
            playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1);
            flickStartTime = Time.time;
            flickTime++;

            if (flickTime >=3)
            {
                setPlayerMode(PlayerState.ALIVE);
                flickTime = 0;
            }
        }
        playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, Mathf.Clamp(playerSprite.color.a - 0.1f, 0.1f,1f));

    }

    void checkEdge()
    {
        Vector2 originalV = rgBody.velocity;
        Vector2 posi = transform.position;
        if (transform.position.x <= minX)
        {
            posi.x = minX;
        }
        else if (transform.position.x >= maxX)
        {
            posi.x = maxX;
        }
        if (transform.position.y <= minY )
        {
            posi.y = minY;
        }
        else if( transform.position.y >= maxY)
        {
            posi.y = maxY;
        }
        rgBody.position = posi;
    }

    public void damage()
    {
        if (state == PlayerState.ALIVE)
        {
            if (life > 0)
            {
                life--;
                setPlayerMode(PlayerState.DAMAGED);
                flickStartTime = Time.time;
            }

            if (life == 0)
            {
                life = 0;
                kill();
            }
            if (hudCtrl != null)
            {
                hudCtrl.setHudLife(life);
            }
        }
        
    }


    void kill()
    {
        setPlayerMode(PlayerState.DEAD);
        gameObject.SetActive(false);
        DestroyObject(gameObject);
        scoreLogic.gameOver();
    }

    public void addLife(int _life)
    {
        life= life+ _life;
        if (hudCtrl != null)
        {
            hudCtrl.setHudLife(life);
        }
    }

    void setPlayerMode(PlayerState _state)
    {
        this.state = _state;
        if (this.state == PlayerState.ALIVE)
        {
            collider.enabled = true;
        }
        else if (this.state == PlayerState.GOD)
        {
            collider.enabled = false;
            godTime = Time.time;
        }
        else if (this.state == PlayerState.DAMAGED)
        {
            collider.enabled = false;
        }
        else if (this.state == PlayerState.DEAD)
        {
            collider.enabled = false;
        }
    }
}
