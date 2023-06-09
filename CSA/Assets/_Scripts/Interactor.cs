using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : Detection
{

    [SerializeField] private PlayerAim crosshair;

    private void Update()
    {
        
    }

    public override void Detect()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("interactable"); 
        
        var interactable = other.GetComponent<IInteractable>();

        if (interactable != null)
        {
            Debug.Log("Interact");
            crosshair.isShooting = false;


            interactable.Interact(this);

        }
    }
}
