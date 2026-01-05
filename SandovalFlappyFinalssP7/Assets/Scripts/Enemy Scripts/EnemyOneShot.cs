using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOneShot : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    [Header("Death Settings")]
    public Animator animator;        
    public float deathDelay = 0.5f;  // time of death animation 

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

  
    public void Die()
    {
        if (isDead) return;
        isDead = true;
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }
        StartCoroutine(DeathRoutine());
    }

    private IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(deathDelay);
        Destroy(gameObject);
    }
}

