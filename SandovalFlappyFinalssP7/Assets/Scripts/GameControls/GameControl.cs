using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{

    public static GameControl instance;
    public GameObject Over;
    public bool gameOver = false;
    private SpriteRenderer sr;
    public float ScrollSpeed = -5f;
    // Start is called before the first frame update
    void Awake ()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver == true && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    public void BirdDied()
    {
        gameOver = true;
        Over.SetActive(true);
    }

}
