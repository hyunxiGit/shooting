using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeBlock : MonoBehaviour {

    SpriteRenderer spriteRender;
    // Use this for initialization
    private void Awake()
    {
        spriteRender = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void setdamage()
    {
        if (spriteRender!=null)
        {
            spriteRender.color = new Color(spriteRender.color.r, spriteRender.color.g, spriteRender.color.b, 0.2f);
        }
    }
}
