using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyHealth : MonoBehaviour
{ // this is for the head guy
    [Header("Movement")]
    public float moveUpDistance = 3f;    
    public float moveSpeed = 2f;           
    [Header("Health")]
    public int maxHealth = 10;
    private int currentHealth;
    [Header("Animations")]
    public Animator animator;             
    [Header("Timing")]
    public float minWait = 10f;         
    public float maxWait = 60f;            
    [Header("Projectile Prefab (optional)")]
    public GameObject projectilePrefab;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isUp) return;
        Projectile proj = other.GetComponent<Projectile>();
        if (proj != null)
        {
            TakeDamage(proj.damage);
            animator.SetTrigger("hurt");
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
            currentHealth = maxHealth; 
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
            while (Vector3.Distance(transform.position, upPos) > 0.01f)
                yield return null;

            while (isUp)
                yield return null;
        }
    }
}
