using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public bool isPaused { get; private set; }
    public static PauseScript instance;
    public GameObject pauseMenuUI;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void stopGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void startGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
    }
}
