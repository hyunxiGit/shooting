using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeProjectileGroup : MonoBehaviour {

    public GameObject[] projectiles;
    public GameObject centerPoint;

    float lifeTime = 8f;
    float destroyTime;
    private void Awake()
    {

    }

    // Use this for initialization
    void Start ()
    {
        for (int i = 0; i < projectiles.Length; i++)
        {
            projectiles[i].GetComponent<BeeProjectile>().setCenter(centerPoint);
        }

        Destroy(centerPoint, lifeTime);
        Destroy(gameObject, lifeTime);
    }
	
	
}
