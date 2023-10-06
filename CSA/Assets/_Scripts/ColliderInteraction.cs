using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColliderInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    public bool requireTag = false; // Should the interaction require a specific tag?
    public string requiredTag = "Player"; // The required tag if requireTag is true.

    [Header("Event Settings")]
    public UnityEvent onEnterEvent; // Events triggered when something enters the collider.
    public UnityEvent onExitEvent; // Events triggered when something exits the collider.

    [Header("Detection Settings")]
    public bool detectOnTriggerEnter = true; // Detect collisions on OnTriggerEnter.
    public bool detectOnTriggerExit = true; // Detect collisions on OnTriggerExit.

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if detection is enabled and conditions are met for entering.
        if (detectOnTriggerEnter && CheckInteractionConditions(collision))
        {
            HandleEnterEvent(); // Trigger the enter event.
        }
        else
        {
            HandleNextLevel();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if detection is enabled and conditions are met for exiting.
        if (detectOnTriggerExit && CheckInteractionConditions(collision))
        {
            HandleExitEvent(); // Trigger the exit event.
        }
    }


    private bool CheckInteractionConditions(Collider2D collision)
    {
        // Check if conditions are met for the interaction based on tag requirement.
        if (requireTag && !collision.CompareTag(requiredTag))
        {
            return false; // Conditions not met.
        }

        return true; // Conditions met.
    }

    private void HandleEnterEvent()
    {
        // Invoke the events associated with entering the collider.
        onEnterEvent?.Invoke();
    }

    private void HandleExitEvent()
    {
        // Invoke the events associated with exiting the collider.
        onExitEvent?.Invoke();
    }

    public void HandleNextLevel()
    {
        GameManager.Instance.LoadLevel();
    }

    private void LateUpdate()
    {

    }
}