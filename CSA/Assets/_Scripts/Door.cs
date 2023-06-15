using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] private bool itemNeeded;
    [SerializeField] private Animator _anim;
    private bool isActive = false;
    
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
            Debug.LogWarning("Item not found");
            if (!isActive && !itemNeeded)
            {
                isActive = true;
                _anim.SetTrigger("Open");
            }
        }
        
    }
}
