using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : Interactable, IInteractable
{
    private FieldOfView fov;
    [SerializeField] private Animator _anim;

    private void Start()
    {
        fov = GetComponent<FieldOfView>();
        Reset();
    }

    private void Update()
    {

    }

    private void LateUpdate()
    {
        if (fov.IsPlayerDetected())
        {
            GameManager.Instance.RestartLevel();
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
