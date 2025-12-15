using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOneShot : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;

    [Header("Death Settings")]
    public Animator animator;         // Assign if you have a death animation
    public float deathDelay = 0.5f;  // Time for death animation

    private bool isDead = false;

    void Update()
    {
        if (!isDead)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);

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

        // Stop movement and play animation if any
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

