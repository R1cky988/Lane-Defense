using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public Transform[] playerPoints;

    public Transform[] enemyPoints;

    [SerializeField] private AudioClip victorySound;
    [SerializeField] private AudioClip gameOverSound;
    public Gameover gameoverScreen;
    public Victory victoryScreen;

    [SerializeField] private SoundMixerManager soundMixerManager;

    private void Awake()
    {
        instance = this;
    }

    public void OnWaveCompleted(int wave)
    {
        if (wave >= 5)
        {
            StartCoroutine(VictoryDelay());
        }
    }

    private IEnumerator VictoryDelay()
    {
        yield return new WaitForSeconds(1.5f);
        Victory();
    }

    public void Victory()
    {
        soundMixerManager.SetPreviousVolume();
        soundMixerManager.SetMusicVolume(0.0001f);
        SFXManager.instance.PlaySFX(victorySound, Camera.main.transform, 0.5f);
    
        Time.timeScale = 0f;
        victoryScreen.Setup();
    }
    public void gameOver()
    {
        soundMixerManager.SetPreviousVolume();
        soundMixerManager.SetMusicVolume(0.0001f);
        SFXManager.instance.PlaySFX(gameOverSound, Camera.main.transform, 0.5f);
        Time.timeScale = 0f; 
        gameoverScreen.Setup();
    }
}
