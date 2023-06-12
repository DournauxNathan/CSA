using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : Interactable, IInteractable
{
    [SerializeField] private FieldOfView fov;
    [SerializeField] private Animator _anim;

    private void Start()
    {
        Reset();
    }

    private void Update()
    {
        if (fov.IsPlayerDetected())
        {
            Debug.LogWarning("Player is detected !");
        }
    }

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
        Toogle();
    }

    public void Timer()
    {
        throw new System.NotImplementedException();
    }

    public void Toogle()
    {
        if (isActive)
        {
            isActive = false;
            _anim.enabled = false;

            StartCoroutine(DecreaseTimer());
            onDeactivate?.Invoke();
        }
        else if (!isActive)
        {
            isActive = true;
            _anim.enabled = true;
            onActivate?.Invoke();
            coolDown = resetTimer;
        }

    }
}
