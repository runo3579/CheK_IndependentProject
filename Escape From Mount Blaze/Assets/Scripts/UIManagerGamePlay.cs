using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManagerGamePlay : MonoBehaviour
{
    public GameObject LevelFailed;
    public void Start()
    {
        Time.timeScale = 1;
    }
    public void RestartScene()
    {
        SceneManager.LoadScene("GamePlay");
    }
    public void LoadAnotherScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
