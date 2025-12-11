using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{

    public float jetpackForce = 10f;      // Force applied when Space is held
    public float maxVerticalSpeed = 10f;  // Optional: limit upward speed

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Hold space to activate jetpack
        if (Input.GetKey(KeyCode.Space))
        {
            // Apply upward force
            rb.AddForce(Vector2.up * jetpackForce, ForceMode2D.Force);

            // Optional: clamp vertical speed
            if (rb.velocity.y > maxVerticalSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, maxVerticalSpeed);
            }
        }
    }
}
