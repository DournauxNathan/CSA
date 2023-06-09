using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

public class Interatable : MonoBehaviour
{
    public bool isActive;
    public float coolDown;

    public UnityEvent onActivate, onDeactivate, onToggle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
