using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUIScript : MonoBehaviour
{
    public GameObject settingUI;
    public void returnGame()
    {
        PauseScript.instance.Resume();
    }

    public void returnMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");

    }

    public void setting()
    {
        gameObject.SetActive(false);
        settingUI.SetActive(true);
    }

    public void playAgain()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        SceneManager.LoadScene("MainGame");
    }
}
