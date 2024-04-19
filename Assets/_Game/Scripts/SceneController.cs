using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    public void PlayLevel1()
    {
        SceneManager.LoadScene("Level1");
    }
    public void PlayLevel2()
    {
        SceneManager.LoadScene("Level2");
    }
    public void PlayLevel3()
    {
        SceneManager.LoadScene("Level3");
    }
    public void PlayLevel4()
    {
        SceneManager.LoadScene("Level4");
    }
    public void PlayLevel5()
    {
        SceneManager.LoadScene("Level5");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        //LevelManagement.instance.Restart();
        SceneManager.LoadScene("MainMenu");
    }
}
