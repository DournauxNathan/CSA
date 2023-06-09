using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour, IInteractable
{
    public string InteractionPrompt => throw new System.NotImplementedException();

    public bool isActive { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public float coolDown { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void Activate()
    {
        throw new System.NotImplementedException();
    }

    public void Deactivate()
    {
        throw new System.NotImplementedException();
    }

    public bool Interact(bool isActive)
    {
        throw new System.NotImplementedException();
    }

    public void Timer()
    {
        throw new System.NotImplementedException();
    }

    public void Toogle()
    {
        throw new System.NotImplementedException();
    }
}
