using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repeat2 : MonoBehaviour
{
    private float length;
    void Start()
    {
        BoxCollider2D gc = GetComponent<BoxCollider2D>();
        length = gc.size.x * transform.localScale.x;
    }
    void Update()
    {
        if (transform.position.x <= -length)
        {
            Reposition();
        }
    }
    void Reposition()
    {
        transform.position += Vector3.right * length * 2.015f;
    }
}