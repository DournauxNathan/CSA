using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Interactable, IInteractable
{
    public void Activate()
    {
        throw new System.NotImplementedException();
    }

    public void Deactivate()
    {
        throw new System.NotImplementedException();
    }

    public void Interact()
    {
        onInteract?.Invoke();
    }

    public void Timer()
    {
        throw new System.NotImplementedException();
    }

    public void Toogle()
    {
        if (isActive)
        {
            Activate();
        }
    }

}
