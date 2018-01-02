using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {
    public float delayTime =1f;

    float killTime;

	// Use this for initialization
	void Start ()
    {
        killTime = Time.time + delayTime;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Time.time > killTime)
        {
            Destroy(gameObject);
        }
	}
}
