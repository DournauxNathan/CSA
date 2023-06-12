using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public bool isActive;
    public float coolDown;
    public float resetTimer { get; private set; }

    public UnityEvent onInteract, onActivate, onDeactivate, onToggle, onTimerEnd;

    public void Reset()
    {
        resetTimer = coolDown;
    }

    public IEnumerator DecreaseTimer()
    {
        while (true)
        {
            yield return null;

            coolDown -= Time.deltaTime;

            if (coolDown <= 0)
            {
                onTimerEnd?.Invoke();
                yield break;
            }
        }
    }

    public void StopTimer()
    {
        StopCoroutine(DecreaseTimer());
    }

}
