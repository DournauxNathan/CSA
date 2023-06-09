using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private PlayerAim crosshair;
    [SerializeField] private Transform hitCollider;

    private Vector3 _startPos;

    private void Start()
    {
        _startPos = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("");

        var interactable = other.GetComponent<IInteractable>();
        hitCollider = other.transform;

        if (interactable != null)
        {
            Debug.Log("Interact");
            crosshair.isShooting = false;


            interactable.Interact();

        }
    }
}
