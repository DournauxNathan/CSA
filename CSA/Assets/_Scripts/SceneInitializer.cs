using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneInitializer : Singleton<SceneInitializer>
{
    public UnityEvent onStartEvent; // UnityEvent to hold your start-of-scene events.

    public void Start()
    {
        // Invoke the UnityEvent to trigger the start-of-scene events.
        onStartEvent.Invoke();
    }

}
