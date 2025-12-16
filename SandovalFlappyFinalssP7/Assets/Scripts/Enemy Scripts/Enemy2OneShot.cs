using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public float speed = 5f;
    public float amplitude = 1f;
    public float frequency = 2f;
    public float deathDelay = 0.5f;
    public Animator animator;
    public GameObject hitEffectPrefab;

    private Rigidbody2D rb;
    private Vector2 startPos;
    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = rb.position;
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (isDead) return;

        float newY = startPos.y + Mathf.Sin(Time.time * frequency) * amplitude;
        Vector2 newPos = new Vector2(rb.position.x - speed * Time.fixedDeltaTime, newY);
        rb.MovePosition(newPos);

        if (rb.position.x < -20f)
            Destroy(gameObject);
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        if (animator != null)
            animator.SetTrigger("Die");

        StartCoroutine(DeathRoutine());
    }

    private IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(deathDelay);
        Destroy(gameObject);
    }

    public void PlayHitEffect()
    {
        if (hitEffectPrefab != null)
        {
            GameObject effect = Instantiate(hitEffectPrefab, rb.position, Quaternion.identity);
            Animator effectAnim = effect.GetComponent<Animator>();
            if (effectAnim != null)
                effectAnim.SetTrigger("Hit");
            Destroy(effect, 0.5f);
        }
    }
}
