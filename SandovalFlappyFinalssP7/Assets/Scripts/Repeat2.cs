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
        // When this piece goes fully off-screen to the left
        if (transform.position.x <= -length)
        {
            Reposition();
        }
    }

    void Reposition()
    {
        // Move it exactly behind the other piece
        transform.position += Vector3.right * length * 2.015f;
    }
}