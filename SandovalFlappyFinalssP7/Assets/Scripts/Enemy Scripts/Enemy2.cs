using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2OneShot : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public float amplitude = 1f;
    public float frequency = 2f;
    private float startY;
    [Header("Death Settings")]
    public Animator animator;        
    public float deathDelay = 0.5f;   
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
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            float newY = startY + Mathf.Sin(Time.time * frequency) * amplitude;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
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
