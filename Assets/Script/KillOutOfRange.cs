using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOutOfRange : MonoBehaviour {

    public GameObject[] killTypes;
    List<string> tags;

    private void Awake()
    {
        if (killTypes.Length >0)
        {
            tags = new List<string>();
            for (int i = 0; i< killTypes.Length; i++)
            {
                tags.Add(killTypes[i].tag);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null)
        {

            Kill killCtrl = collision.gameObject.GetComponent<Kill>();
            if (killCtrl != null)
            {
                if (killTypes.Length ==0)
                {
                    killCtrl.kill(false);
                }
                else
                {
                    if (tags.Contains(collision.gameObject.tag))
                    {
                        killCtrl.kill();
                    }
                }
                
            }
        }
      
    }
}
