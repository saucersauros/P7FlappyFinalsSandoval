using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrollspikes : MonoBehaviour
{
    public float speed = 5f; // units per second

    void Update()
    {
        // Move left
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        // Destroy if x < -20
        if (transform.position.x < -20f)
        {
            Destroy(gameObject);
        }
    }
}