using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    [SerializeField] private PlayerAim crosshair;

    public float radius;
    public LayerMask detectables;

    private Collider2D[] hitColliders;

    // Update is called once per frame
    void Update()
    {
        if (crosshair.isAimMode)
        {
            Detect();   
        }
    }

    public void Detect()
    {
        hitColliders = Physics2D.OverlapCircleAll(this.transform.position, radius, detectables);

        if (hitColliders.Length < 0)
        {
            return;
        }

        Debug.Log(hitColliders.Length);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Interactable"))
            {
                crosshair.GetTargetInfo(hitCollider.transform);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, radius);
    }

}
