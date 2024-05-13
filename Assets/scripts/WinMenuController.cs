// Hunter M 5/12/2024
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenuController : MonoBehaviour
{
    public void ReplayScene()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
}