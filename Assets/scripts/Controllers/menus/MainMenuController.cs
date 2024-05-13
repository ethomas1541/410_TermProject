// based on https://www.youtube.com/watch?v=nUNHJMdDuXE&ab_channel=GAW
// implemented by Hunter M 5/8/2024
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void PlayLV0()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayLV1()
    {
         SceneManager.LoadScene(2);
    }

    public void PlayLV2()
    {
         SceneManager.LoadScene(3);
    }

    public void PlayLV3()
    {
         SceneManager.LoadScene(4);
    }
}
