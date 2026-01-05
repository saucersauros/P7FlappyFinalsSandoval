using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [Header("Score Display")]
    public GameObject[] numberPrefabs;
    public Transform scoreParent;      
    public float digitSpacing = 0.5f;  
    private int score = 0;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreDisplay();
    }
    void UpdateScoreDisplay()
    {
        foreach (Transform child in scoreParent)
        {
            Destroy(child.gameObject);
        }
        string scoreStr = score.ToString();
        for (int i = 0; i < scoreStr.Length; i++)
        {
            int digit = int.Parse(scoreStr[i].ToString());
            GameObject numberGO = Instantiate(numberPrefabs[digit], scoreParent);
            numberGO.transform.localPosition = new Vector3(i * digitSpacing, 0f, 0f);
        }
    }
}