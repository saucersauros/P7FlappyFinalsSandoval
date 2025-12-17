using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Movement")]
    public float jetpackForce = 10f;
    public float maxVerticalSpeed = 10f;
    public Animator anim;

    [Header("Shooting")]
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
            HandleJetpack();
            HandleShooting();
        }
        if (Input.anyKeyDown)
        {
            Time.timeScale = 1f;
        }
    }

    void HandleJetpack()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jetpackForce, ForceMode2D.Force);

            if (rb.velocity.y > maxVerticalSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, maxVerticalSpeed);
            }
        }
    }

    void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Shoot();
            anim.SetTrigger("Shoot");
        }
    }

    void Shoot()
    {
        Vector2 offset = shootOffset;
        offset.x *= shootDirection;

        Vector2 spawnPosition = (Vector2)transform.position + offset;

        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
        projectile.GetComponent<Projectile>().SetDirection(Vector2.right * shootDirection);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // POINT / HITBOX
        if (other.CompareTag("Hitbox") || other.CompareTag("Point"))
        {
            Debug.Log("Point collected!");
            // Add scoring logic here, e.g. ScoreManager.Instance.AddScore(1);

        }
        // DEADLY OBJECTS
        else if (other.CompareTag("Deadly") || other.transform.root.CompareTag("Deadly"))
        {
            Die();
            Time.timeScale = 0f;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Hitbox"))
        {
            anim.SetBool("walk", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Hitbox"))
        {
            anim.SetBool("walk", false);
        }
    }

    void Die()
    {
        if (!isDead)
        {
            anim.SetBool("Death", true);
            GameControl.instance.BirdDied();
            isDead = true;
        }
    }
}