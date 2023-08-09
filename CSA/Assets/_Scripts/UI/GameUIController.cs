using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    [Header("Paused Screen")]
    public Button resumeButton;
    public Button skipButton;
    public Button quitButton;

    public GameObject pausedScreenPanel;

    public OptionsUIController options;

    private void Start()
    {
        resumeButton.onClick.AddListener(Resume);
        skipButton.onClick.AddListener(Skip);
        quitButton.onClick.AddListener(() => GameManager.Instance.Quit());

        pausedScreenPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !options.optionsPanel.activeSelf)
        {
            Pause();
        }
    }

    private void Pause()
    {
        pausedScreenPanel.SetActive(true);
    }
    
    private void Resume()
    {
        pausedScreenPanel.SetActive(false);
    }

    private void Skip()
    {

    }

    private void Quit()
    {
        GameManager.Instance.LoadLevel(0);
    }

}
