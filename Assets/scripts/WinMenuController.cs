// Hunter M 6/5/2024
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenuController : MonoBehaviour
{
    public void ReplayScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();s
        SceneManager.LoadScene(currentScene.name);
        Time.timeScale = 1f;
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
}