using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth  : MonoBehaviour
{
    [Header("Movement")]
    public float moveUpDistance = 3f;       // How far it pops up
    public float moveSpeed = 2f;            // Speed of moving up/down

    [Header("Health")]
    public int maxHealth = 10;
    private int currentHealth;

    [Header("Animations")]
    public Animator animator;               // Assign Animator with "Search" and "Hit"

    [Header("Timing")]
    public float minWait = 10f;             // Minimum time to reappear
    public float maxWait = 60f;             // Maximum time to reappear

    private Vector3 startPos;
    private Vector3 upPos;
    private bool isUp = false;
    private bool isMoving = false;

    void Start()
    {
        startPos = transform.position;
        upPos = startPos + Vector3.up * moveUpDistance;
        currentHealth = maxHealth;

        StartCoroutine(PopupRoutine());
    }

    void Update()
    {
        // Smoothly move up or down
        if (isMoving)
        {
            Vector3 target = isUp ? upPos : startPos;
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

            // Stop moving when reached target
            if (Vector3.Distance(transform.position, target) < 0.01f)
                isMoving = false;
        }
    }

    // Called by projectile
    public void TakeDamage(int damage)
    {
        if (!isUp) return; // only take damage when up
        currentHealth -= damage;

        if (animator != null)
            animator.SetTrigger("Hit");

        if (currentHealth <= 0)
        {
            // Move back down
            isUp = false;
            isMoving = true;
            currentHealth = maxHealth; // reset for next popup
        }
    }

    private IEnumerator PopupRoutine()
    {
        while (true)
        {
            // Wait a random time between min/max
            float waitTime = Random.Range(minWait, maxWait);
            yield return new WaitForSeconds(waitTime);

            // Pop up
            isUp = true;
            isMoving = true;

            // Play search animation
            if (animator != null)
                animator.SetTrigger("Search");

            // Wait until fully up
            while (Vector3.Distance(transform.position, upPos) > 0.01f)
                yield return null;

            // Wait until health reaches 0 (projectile hits)
            while (isUp)
                yield return null;
        }
    }
}

