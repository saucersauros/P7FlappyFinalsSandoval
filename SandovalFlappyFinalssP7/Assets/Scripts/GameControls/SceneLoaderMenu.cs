using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderMenu : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene("Game");
    }

    public void TestClick()
    {
        Debug.Log("Button clicked");
    }

}
