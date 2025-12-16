using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyHealth : MonoBehaviour
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

    [Header("Projectile Prefab (optional)")]
    public GameObject projectilePrefab;     // Reference to projectile if needed elsewhere

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
        if (isMoving)
        {
            Vector3 target = isUp ? upPos : startPos;
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, target) < 0.01f)
                isMoving = false;
        }
    }

    // Only takes damage from objects with a Projectile script
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isUp) return; // Only take damage when up

        // Check for Projectile component
        Projectile proj = other.GetComponent<Projectile>();
        if (proj != null)
        {
            TakeDamage(proj.damage);
            animator.SetTrigger("hurt");

            // Destroy projectile after hitting
            Destroy(other.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (animator != null)
            animator.SetTrigger("Hit");

        if (currentHealth <= 0)
        {
            isUp = false;
            isMoving = true;
            currentHealth = maxHealth; // reset for next popup
            animator.SetTrigger("HURT");
        }
    }

    private IEnumerator PopupRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(minWait, maxWait);
            yield return new WaitForSeconds(waitTime);

            isUp = true;
            isMoving = true;


            // Wait until fully up
            while (Vector3.Distance(transform.position, upPos) > 0.01f)
                yield return null;

            // Wait until health reaches 0 (hit by projectile)
            while (isUp)
                yield return null;
        }
    }
}
