using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LaserManager : MonoBehaviour
{
    [SerializeField] private List<Laser> lasers;
    public UnityEvent onActive, onDeactivate;

    private void Start()
    {
        Active();
    }

    public void Active()
    {
        onActive?.Invoke();

        foreach (var item in lasers)
        {
            item.gameObject.SetActive(true);
        }
    }

    public void Deactivate()
    {
        onDeactivate?.Invoke();

        foreach (var item in lasers)
        {
            item.gameObject.SetActive(false);
        }
    }
}
