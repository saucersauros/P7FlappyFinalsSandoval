using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    [Header("Score Settings")]
    public int points = 1;
    [Header("Target Prefab")]
    public GameObject targetPrefab;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (targetPrefab != null && other.gameObject.name.StartsWith(targetPrefab.name))
        {
            ScoreManager.Instance.AddScore(points);
            Destroy(gameObject);
        }
    }
}