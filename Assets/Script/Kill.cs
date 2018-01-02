using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour {

    public ParticleSystem dieParticle;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void kill(bool useParticle = true)
    {
        if (dieParticle!=null && useParticle)
        {
            Instantiate(dieParticle,transform.position,transform.rotation);
        }
        Destroy(gameObject);
    }
}
