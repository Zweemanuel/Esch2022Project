using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void switchToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void switchToMap()
    {
        SceneManager.LoadScene(1);
    }


    public void switchToCam()
    {
        SceneManager.LoadScene(2);
    }

    public void switchToQuiz()
    {
        SceneManager.LoadScene(3);
    }

    public void switchToAlbum()
    {
        SceneManager.LoadScene(4);
    }
}
