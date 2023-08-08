using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] private bool itemNeeded;
    [SerializeField] private Animator _anim;
    private bool isActive = false;

    [SerializeField] private bool callAnimationOver = false;


    [ContextMenu("Open")]
    public void Open()
    {
        if (Inventory.GetItemList() != null)
        {
            if (!isActive && itemNeeded && Inventory.GetItemList().Contains(item))
            {
                isActive = true;
                _anim.SetTrigger("Open");
            }
            else if (!isActive && !itemNeeded)
            {
                isActive = true;
                _anim.SetTrigger("Open");
            }
        }
        else
        {
            //Debug.LogWarning("Item not found");
            if (!isActive && !itemNeeded)
            {
                isActive = true;
                _anim.SetTrigger("Open");
            }
        }
        
    }

    // This method can be called by an Animator Event at the end of the animation.
    public void AnimationOver()
    {
        if (callAnimationOver)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.LoadLevel();
            }
        }
    }
}


