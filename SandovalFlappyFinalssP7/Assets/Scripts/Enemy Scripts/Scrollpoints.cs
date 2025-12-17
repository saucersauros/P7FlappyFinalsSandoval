using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrollpoints : MonoBehaviour
{
    public float speed = 5f;
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        if (transform.position.x < -20f)
        {
            Destroy(gameObject);
        }
    }
}