using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius;
    [Range(1, 360)] public float angle;
    public LayerMask targetMask;
    public LayerMask obstuctionLayer;

    private Transform target;

    public bool canSeePlayer { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FOVCheck());
    }

    IEnumerator FOVCheck()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FOV();
        }
    }

    private void FOV()
    {
        Collider2D rangeCheck = Physics2D.OverlapCircle(transform.position, radius, targetMask);

        if (rangeCheck != null)
        {
            target = rangeCheck.transform;
            Vector2 directionTarget = (target.position - transform.position).normalized;
            
            if (Vector2.Angle(transform.forward, directionTarget) < angle / 2f)
            {
                float distance = Vector2.Distance(transform.position, target.position);

                if (!Physics2D.Raycast(transform.position, target.position, distance, obstuctionLayer))
                {
                    canSeePlayer = true;
                }
                else
                    canSeePlayer = false;
            }
            else
            {
                canSeePlayer = false;
            }
        }
        else if (canSeePlayer)
        {
            target = null;
            canSeePlayer = false;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, radius);

        Vector3 angleA = DirectionFromAngle(-transform.eulerAngles.z, -angle / 2);
        Vector3 angleB = DirectionFromAngle(-transform.eulerAngles.z, angle / 2);

        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, transform.position + angleA * radius);
        Gizmos.DrawLine(transform.position, transform.position + angleB * radius);

        if (canSeePlayer)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, target.position);
        }

    }

    private Vector2 DirectionFromAngle(float eulerY, float angleInDeg)
    {
        angleInDeg += eulerY;
        return new Vector2(Mathf.Sin(angleInDeg * Mathf.Deg2Rad), Mathf.Cos(angleInDeg * Mathf.Deg2Rad));
    }

    public Vector3 GetVectorFromAngles(float angle)
    {
        //Angle = 0 -> 360° 
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    public float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

}
