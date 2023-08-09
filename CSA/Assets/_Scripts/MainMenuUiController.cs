using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using System.Linq;


public class MainMenuUiController : MonoBehaviour
{
    public Button playButton;
    public Button optionsButton;
    public Button quitButton;
    public GameObject optionsPanel;
    public Button[] optionsButtons;
    public GameObject[] optionsTabs;

    private void Start()
    {
        playButton.onClick.AddListener(PlayGame);
        optionsButton.onClick.AddListener(ShowOptionsPanel);
        quitButton.onClick.AddListener(() => GameManager.Instance.Quit());

        for (int i = 0; i < optionsButtons.Length; i++)
        {           
            Debug.Log("optionsButtons[" + i + "].onClick.AddListener(() => DisplayOptionTab(" + i + "));");
        }


        optionsButtons[0].onClick.AddListener(() => DisplayOptionTab(0));
        optionsButtons[1].onClick.AddListener(() => DisplayOptionTab(1));
        optionsButtons[2].onClick.AddListener(() => DisplayOptionTab(2));
        optionsButtons[3].onClick.AddListener(() => DisplayOptionTab(3));
        optionsButtons[4].onClick.AddListener(() => DisplayOptionTab(4));
        optionsButtons[5].onClick.AddListener(() => DisplayOptionTab(5));


        for (int i = 0; i < optionsTabs.Length; i++)
        {
            optionsTabs[i].SetActive(false);
        }

        HideOptionsPanel();


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && optionsButton.IsActive())
        {
            HideOptionsPanel();
        }
    }

    private void PlayGame()
    {
        GameManager.Instance.LoadLevel(GameManager.Instance.levelToLoad);
    }

    private void ShowOptionsPanel()
    {
        optionsPanel.SetActive(true);
    }

    private void HideOptionsPanel()
    {
        optionsPanel.SetActive(false);
    }

    private void DisplayOptionTab(int index)
    {
        for (int i = 0; i < optionsTabs.Length; i++)
        {
            if (i == index)
            {
                optionsTabs[i].SetActive(true);
            }
            else
            {
                optionsTabs[i].SetActive(false);
            }
        }
    }
}
