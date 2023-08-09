using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUIController : MonoBehaviour
{
    [Header("Options")]
    public Button optionsButton;
    public GameObject optionsPanel;
    public Button[] optionsButtons;
    public GameObject[] optionsTabs;

    // Start is called before the first frame update
    void Start()
    {
        optionsButton.onClick.AddListener(ShowOptionsPanel);

        /*for (int i = 0; i < optionsButtons.Length; i++)
        {
            
        }*/

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
        if (Input.GetKeyDown(KeyCode.Escape) && optionsPanel.activeSelf)
        {
            HideOptionsPanel();
        }
    }

    private void ShowOptionsPanel()
    {
        Debug.Log("");
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
