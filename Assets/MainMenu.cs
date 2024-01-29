using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("BasementMain");
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("MenuQuit");
    }

    public static int yes = 0;
    public void PresYes()
    {
        Debug.Log("Exit");
        yes++;
    }

    public void PresNo()
    {
        SceneManager.LoadScene("Menu");
        Debug.Log("Continue");
    }
}