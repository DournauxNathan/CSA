using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public Transform sprite;
    public float radius;
    [Range(1, 360)] public float angle;
    public LayerMask targetMask;
    public LayerMask obstuctionLayer;

    private Transform target;

    private bool isPaused;

    public bool canSeePlayer { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FOVCheck());
    }

    IEnumerator FOVCheck()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (!isPaused)
        {
            yield return wait;
            FOV();
        }
    }

    private void FOV()
    {
        Collider2D rangeCheck = Physics2D.OverlapCircle(sprite.position, radius, targetMask);

        if (rangeCheck != null)
        {
            target = rangeCheck.transform;
            Vector2 directionToTarget = (target.position - sprite.position).normalized;

            if (Vector2.Angle(sprite.rotation.y == 180 ? -sprite.right : sprite.right, directionToTarget) < angle / 2)
            {
                float distance = Vector2.Distance(sprite.position, target.position);

                if (!Physics2D.Raycast(sprite.position, directionToTarget, distance, obstuctionLayer))
                {
                    canSeePlayer = true;
                }
                else
                {
                    target = null;
                    canSeePlayer = false;
                }
            }
            else
            {
                target = null;
                canSeePlayer = false;
            }
        }
        else if (canSeePlayer)
        {
            target = null;
            canSeePlayer = false;
        }

    }

    public bool IsPlayerDetected()
    {
        return canSeePlayer;
    }

    private void OnDrawGizmos()
    {
        UnityEditor.Handles.color = Color.white;

        Vector3 angleA = DirectionFromAngle(-angle / 2);
        Vector3 angleB = DirectionFromAngle(angle / 2);

        UnityEditor.Handles.DrawWireArc(sprite.position, Vector3.forward, angleA, angle, radius, 2f);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(sprite.position, sprite.position + angleA * radius);
        Gizmos.DrawLine(sprite.position, sprite.position + angleB * radius);

        if (canSeePlayer)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(sprite.position, target.position);
        }

    }

    public void Pause(bool value)
    {
        isPaused = value;

        if (!isPaused)
        {
            StartCoroutine(FOVCheck());
        }
    }

    private Vector2 DirectionFromAngle(float angleInDegrees)
    {
        return (Vector2)(Quaternion.Euler(0, 0, angleInDegrees) * (sprite.rotation.y == 180 ? -sprite.right : sprite.right));
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
