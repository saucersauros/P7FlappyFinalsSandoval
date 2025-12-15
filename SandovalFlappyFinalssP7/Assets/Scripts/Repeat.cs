using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repeat : MonoBehaviour
{
    private BoxCollider2D gc;
    private float ghl;
    public float f;
    public float fs;


// gc = ground offset
// ghl = ground horizontal length
// go = ground offset
// rpb = repositonbackround


    void Start()
    {
        gc = GetComponent<BoxCollider2D>();
        ghl = gc.size.x;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x + f < -ghl)
        {
            rpb ();
        }
        
    }
    private void rpb()
    {
        Vector2 go = new Vector2(ghl * fs, 0);
        transform.position = (Vector2) transform.position + go;

    }
}
