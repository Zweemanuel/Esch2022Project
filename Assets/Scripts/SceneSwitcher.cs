using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{

    // 0 - main / 1 - map / 2 - cam / 3 - quiz / 4 - album
    public void switchToScene(int n)
    {
        SceneManager.LoadScene(n);
    }

}
