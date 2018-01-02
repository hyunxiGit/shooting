using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropItem : MonoBehaviour {

    public GameObject item;

    public void drop()
    {
        if (item!=null)
        {
            Instantiate(item,transform.position,transform.rotation);
        }
    }
}
