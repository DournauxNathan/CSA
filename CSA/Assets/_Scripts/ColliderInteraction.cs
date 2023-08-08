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

    private void OnTriggerEnter(Collider other)
    {
        // Check if detection is enabled and conditions are met for entering.
        if (detectOnTriggerEnter && CheckInteractionConditions(other))
        {
            HandleEnterEvent(); // Trigger the enter event.
        }
        else
        {
            HandleNextLevel();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if detection is enabled and conditions are met for exiting.
        if (detectOnTriggerExit && CheckInteractionConditions(other))
        {
            HandleExitEvent(); // Trigger the exit event.
        }
    }

    private bool CheckInteractionConditions(Collider other)
    {
        // Check if conditions are met for the interaction based on tag requirement.
        if (requireTag && !other.CompareTag(requiredTag))
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
}