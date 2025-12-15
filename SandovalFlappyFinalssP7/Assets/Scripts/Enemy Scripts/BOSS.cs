using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float length;

    [Header("Y Range")]
    public float minY = 0.46f;
    public float maxY = -8.19f;

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
        float randomY = Random.Range(minY, maxY);

        transform.position = new Vector3(
            transform.position.x + length * 2.015f,
            randomY,
            transform.position.z
        );
    }
}
