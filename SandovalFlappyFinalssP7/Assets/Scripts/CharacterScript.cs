using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{

    public float jetpackForce = 10f;      // Force applied when Space is held
    public float maxVerticalSpeed = 10f;  // Optional: limit upward speed
    public Animator anim;
    public GameObject hitbox;
    public GameObject spike;
    private bool isDead = false;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isDead == false)
        {
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


    // Called when the player enters the hitbox
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == hitbox)
        {
            anim.SetBool("walk", true);
        }
        else 
        {
            anim.SetBool("Death", true);
            GameControl.instance.BirdDied();
            isDead = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject == hitbox)
        {
            anim.SetBool("walk", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == hitbox)
        {
            anim.SetBool("walk", false);
        }
    }
}


