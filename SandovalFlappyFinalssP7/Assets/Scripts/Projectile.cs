using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 5f;
    public int damage = 1;

    void Start()
    {
        // Auto destroy after lifeTime
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Move forward (right)
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // One-shot enemies
        EnemyOneShot oneShot = other.GetComponent<EnemyOneShot>();
        if (oneShot != null)
        {
            oneShot.Die();
            Destroy(gameObject);
            return;
        }

        // Health enemy
        EnemyHealth healthEnemy = other.GetComponent<EnemyHealth>();
        if (healthEnemy != null)
        {
            healthEnemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
