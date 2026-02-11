using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingUI;
    public void StartButton()
    {
       SceneManager.LoadScene("MainGame");
    }

    public void SettingButton()
    {
        settingUI.SetActive(true);
    }
    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Game Exited");
    }
    

}
