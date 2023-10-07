using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FieldOfView : MonoBehaviour
{
    public Transform sprite;
    public float radius;
    [Range(1, 360)] public float angle;
    [Range(3, 48)] public int smoothFactor = 8;
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
        Collider2D rangeCheck = Physics2D.OverlapCircle(Vector3.zero, radius, targetMask);

        if (rangeCheck != null)
        {
            target = rangeCheck.transform;
            Vector2 directionToTarget = (target.position - Vector3.zero).normalized;

            if (Vector2.Angle(sprite.rotation.y == 180 ? -sprite.right : sprite.right, directionToTarget) < angle / 2)
            {
                float distance = Vector2.Distance(Vector3.zero, target.position);

                if (!Physics2D.Raycast(Vector3.zero, directionToTarget, distance, obstuctionLayer))
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

    public void Pause(bool value)
    {
        isPaused = value;

        if (!isPaused)
        {
            StartCoroutine(FOVCheck());
        }
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
