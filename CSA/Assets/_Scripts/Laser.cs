using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private LineRenderer lr;
    public Transform _transform;

    public LayerMask mask;
    public float distance;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        _transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        lr.SetPosition(0, transform.position);

        if (Physics2D.Raycast(_transform.position, transform.right, mask))
        {
            RaycastHit2D hit = Physics2D.Raycast(_transform.position, transform.right, mask);

            if (hit.collider)
            {
                Draw2DRay(_transform.position, hit.point);

                if (hit.collider.CompareTag("Player"))
                {
                    GameManager.Instance.RestartLevel();
                }
            }
        }
    }

    void Draw2DRay(Vector2 startpos, Vector2 endPos)
    {
        lr.SetPosition(0, startpos);
        lr.SetPosition(1, endPos);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * 5f);
    }

}
