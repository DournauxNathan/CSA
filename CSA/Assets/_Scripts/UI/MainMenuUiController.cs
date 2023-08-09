using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class MainMenuUiController : MonoBehaviour
{
    [Header("Main Menu")]
    public Button playButton;
    public Button quitButton;

    private void Start()
    {
        playButton.onClick.AddListener(PlayGame);
        quitButton.onClick.AddListener(() => GameManager.Instance.Quit());        
    }

    private void PlayGame()
    {
        GameManager.Instance.LoadLevel();
    }
}
