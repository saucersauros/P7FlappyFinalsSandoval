using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 5f;
    public int damage = 1;

    private Vector2 moveDirection;

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the enemy has EnemyMovement script
        Enemy2 enemyMovement = other.GetComponent<Enemy2>();
        if (enemyMovement != null)
        {
            Destroy(gameObject);
            enemyMovement.Die();
            enemyMovement.PlayHitEffect(); 
            return;
        }

        // Optional: still support other health-based enemies
        EnemyHealth healthEnemy = other.GetComponent<EnemyHealth>();
        if (healthEnemy != null)
        {
            healthEnemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
