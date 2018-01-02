using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedTexture : MonoBehaviour {

    public GameObject starplane0;
    private Mesh staPplaneMesh0;
    public float speed0;

    public GameObject starplane1;
    private Mesh staPplaneMesh1;
    public float speed1;

    // Use this for initialization
    private void Awake()
    {
        staPplaneMesh0 = starplane0.GetComponent<MeshFilter>().mesh;
        staPplaneMesh1 = starplane1.GetComponent<MeshFilter>().mesh;
    }
	
	// Update is called once per frame
	void Update ()
    {
        loopUV();
    }

    void loopUV()
    {
        Vector2[] uvArray0 = staPplaneMesh0.uv;
        for (int i=0; i< uvArray0.Length; i++)
        {
            uvArray0[i] += new Vector2(0, speed0 * Time.deltaTime);
        }
        staPplaneMesh0.uv = uvArray0;

        Vector2[] uvArray1 = staPplaneMesh1.uv;
        for (int i = 0; i < uvArray1.Length; i++)
        {
            uvArray1[i] += new Vector2(0, speed1 * Time.deltaTime);
        }
        staPplaneMesh1.uv = uvArray1;
    }
}
