using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Gameover : MonoBehaviour
{

    [SerializeField] private SoundMixerManager soundMixerManager;

    public void Setup()
    {
        gameObject.SetActive(true);
    }

    public void RestartButton()
    {
        soundMixerManager.LoadPreviousVolumes();
        SceneManager.LoadScene("MainGame");
    }
    public void MainMenuButton()
    {
        soundMixerManager.LoadPreviousVolumes();
        SceneManager.LoadScene("MainMenu");
    }


}
