using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float jetpackForce = 10f;
    public float maxVerticalSpeed = 10f;
    public Animator anim;
    public GameObject hitbox;
    public GameObject spike;

    // SHOOTING
    public GameObject projectilePrefab;
    public Vector2 shootOffset = new Vector2(1f, 0f); // X & Y spawn offset
    public float shootDirection = 1f; // 1 = right, -1 = left

    private bool isDead = false;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isDead)
        {
            // Jetpack
            if (Input.GetKey(KeyCode.Space))
            {
                rb.AddForce(Vector2.up * jetpackForce, ForceMode2D.Force);

                if (rb.velocity.y > maxVerticalSpeed)
                {
                    rb.velocity = new Vector2(rb.velocity.x, maxVerticalSpeed);
                }
            }

            // SHOOTING (K)
            if (Input.GetKeyDown(KeyCode.K))
            {
                Shoot();
                anim.SetTrigger("Shoot");
            }
        }
    }

    void Shoot()
    {
        // Adjust offset based on facing direction
        Vector2 offset = shootOffset;
        offset.x *= shootDirection;

        Vector2 spawnPosition = (Vector2)transform.position + offset;

        GameObject projectile = Instantiate(
            projectilePrefab,
            spawnPosition,
            Quaternion.identity
        );

        // Set projectile direction
        projectile.GetComponent<Projectile>()
                  .SetDirection(Vector2.right * shootDirection);
    }

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
