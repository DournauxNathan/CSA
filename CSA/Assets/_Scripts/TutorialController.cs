using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialController : MonoBehaviour
{
    [System.Serializable]
    public class InputDisplayer
    {
        public Sprite image;
        public string text;
    }

    public Animator m_Animator;

    public Image image;
    public TextMeshProUGUI text;

    public List<InputDisplayer> inputsToDisplay = new List<InputDisplayer>();

    public void Show()
    {
        m_Animator.SetTrigger("Show");
    }

    public void Hide()
    {
        m_Animator.SetTrigger("Hide");
    }

    public void UpdateTutorialDisplayer(int i)
    {
        if (image == null)
        {
            image.CrossFadeAlpha(0, 0.1f, true) ;
        }
        else
        {
            image.CrossFadeAlpha(1, 0.1f, true);

            image.sprite = inputsToDisplay[i].image;
        }

        text.text = inputsToDisplay[i].text;
    }

}