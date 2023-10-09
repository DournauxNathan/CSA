using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    private Mesh mesh;
    /*private Vector3 origin;
    private float startingAngle;

    [Range(1f, 360f)] public float fov;
    public float viewDistance;
    [Range(3, 48)] public int rayCount = 8;
    public LayerMask targetMask;
    */
    public LayerMask layerMask;
    private bool isPaused;
    public bool debugMesh;

    public bool canSeePlayer { get; private set; }

    // Start is called before the first frame update
    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    private void Update()
    {
        float fov = 90f;
        Vector3 origin = Vector3.zero;
        int rayCount = 50;
        float angle = 0f;
        float angleIncrease = fov / rayCount;
        float viewDistance = 50f;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int trianglesIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;

            #region Collision
            RaycastHit2D raycasthit2D = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance, layerMask);

            if (raycasthit2D.collider == null)
            {
                //No hit
                vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                //Hit object
                vertex = raycasthit2D.point;
            }
            #endregion

            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[trianglesIndex + 0] = 0;
                triangles[trianglesIndex + 1] = vertexIndex - 1;
                triangles[trianglesIndex + 2] = vertexIndex;

                #region Debug Triangles Mesh
                if (debugMesh)
                {
                    int vertexIndexA = triangles[trianglesIndex + 0];
                    int vertexIndexB = triangles[trianglesIndex + 1];
                    int vertexIndexC = triangles[trianglesIndex + 2];

                    Vector3 vertexA = vertices[vertexIndexA];
                    Vector3 vertexB = vertices[vertexIndexB];
                    Vector3 vertexC = vertices[vertexIndexC];

                    // Draw lines to represent the triangles
                    Debug.DrawLine(vertexA, vertexB, Color.blue, 0.1f);
                    Debug.DrawLine(vertexB, vertexC, Color.blue, 0.1f);
                    Debug.DrawLine(vertexC, vertexA, Color.blue, 0.1f);
                }
                #endregion

                trianglesIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrease;
        }


        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        /*mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        origin = Vector3.zero;*/
    }

    /*void LateUpdate()
    {
        float angle = startingAngle;
        float angleIncrease = fov / rayCount;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = this.transform.position;

        int vertexIndex = 1;
        int trianglesIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;

            #region Collision
            RaycastHit2D raycasthit2D = Physics2D.Raycast(origin, GetVectorFromAngles(angle), viewDistance, layerMask);

            if (raycasthit2D.collider == null)
            {
                //No hit
                vertex = origin + GetVectorFromAngles(angle) * viewDistance;
            }
            else
            {
                //Hit object
                vertex = raycasthit2D.point;
            }
            #endregion

            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[trianglesIndex + 0] = 0;
                triangles[trianglesIndex + 1] = vertexIndex - 1;
                triangles[trianglesIndex + 2] = vertexIndex;

                #region Debug Triangles Mesh
                if (debugMesh)
                {
                    int vertexIndexA = triangles[trianglesIndex + 0];
                    int vertexIndexB = triangles[trianglesIndex + 1];
                    int vertexIndexC = triangles[trianglesIndex + 2];

                    Vector3 vertexA = vertices[vertexIndexA];
                    Vector3 vertexB = vertices[vertexIndexB];
                    Vector3 vertexC = vertices[vertexIndexC];

                    // Draw lines to represent the triangles
                    Debug.DrawLine(vertexA, vertexB, Color.blue, 0.1f);
                    Debug.DrawLine(vertexB, vertexC, Color.blue, 0.1f);
                    Debug.DrawLine(vertexC, vertexA, Color.blue, 0.1f);
                }
                #endregion


                trianglesIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrease;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }*/





    public bool IsPlayerDetected()
    {
        return canSeePlayer;
    }

    public void Pause(bool value)
    {
        isPaused = value;
    }
    /*
    public void SetOrigin(Vector3 origin)
    {
        this.origin = origin;
    }

    public void SetAimDirection( Vector3 aimDirection)
    {
        startingAngle = GetAngleFromVectorFloat(aimDirection) - fov/ 2f;
    }*/

    public Vector3 GetVectorFromAngle(float angle)
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
