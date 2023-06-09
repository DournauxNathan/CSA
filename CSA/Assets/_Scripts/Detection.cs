using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    [Header("")]
    public LayerMask layerDetection;
    public float radius;
    public Color color;

    private Vector3 _startPos;
    public Vector3 startPos { get { return _startPos; } set { _startPos = value; }}

    private void Start()
    {
        startPos = transform.position;
    }


    public virtual void Detect()
    {
        if (GetHitInfo() == null) return;

        Debug.Log(GetHitInfo().name);
    }

    public Collider2D GetHitInfo()
    {
        return Physics2D.OverlapCircle(this.transform.position, radius, layerDetection);
    }

    private void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = color;
        Gizmos.DrawWireSphere(this.transform.position, radius);
    }
}
