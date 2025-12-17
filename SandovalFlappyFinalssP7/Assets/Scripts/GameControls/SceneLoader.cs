using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene("Menu");
    }

    public void TestClick()
    {
        Debug.Log("Button clicked");
    }

}
