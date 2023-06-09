using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : Interatable, IInteractable
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
        onActivate?.Invoke();
    }

    public void Timer()
    {
        throw new System.NotImplementedException();
    }

    public void Toogle()
    {
        isActive = !isActive;

        throw new System.NotImplementedException();
    }
}
