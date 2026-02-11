using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScreen : MonoBehaviour
{
    public PauseScript pauseScript;
    void Start()
    {
        PauseScript.instance.stopGame();
    }
    public void CloseTutorial()
    {
        PauseScript.instance.startGame();
        gameObject.SetActive(false);
    }
}
