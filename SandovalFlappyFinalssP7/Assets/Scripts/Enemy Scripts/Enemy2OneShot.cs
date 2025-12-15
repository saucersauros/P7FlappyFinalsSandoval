using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public float amplitude = 1f;
    public float frequency = 2f;

    private float startY;

    [Header("Death Settings")]
    public Animator animator;          // Assign enemy's animator
    public float deathDelay = 0.5f;    // Time for death animation

    private bool isDead = false;
    private float xStart;

    void Start()
    {
        startY = transform.position.y;
        xStart = transform.position.x;
    }

    void Update()
    {
        if (!isDead)
        {
            // Move left
            transform.Translate(Vector2.left * speed * Time.deltaTime);

            // Sinusoidal Y movement
            float newY = startY + Mathf.Sin(Time.time * frequency) * amplitude;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            // Destroy if offscreen
            if (transform.position.x < -20f)
            {
                Destroy(gameObject);
            }
        }
    }

    // Call this when hit by a projectile
    public void Die()
    {
        if (isDead) return;
        isDead = true;

        // Stop movement and play death animation
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        // Destroy after delay
        StartCoroutine(DeathRoutine());
    }

    private IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(deathDelay);
        Destroy(gameObject);
    }
}
