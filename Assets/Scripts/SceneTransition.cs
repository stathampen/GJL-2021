using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadGameScene()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        SceneManager.LoadScene(1);
    }

    public void LoadMenuScene()
    {
        Cursor.visible = true;
        SceneManager.LoadScene(0);
    }
}
