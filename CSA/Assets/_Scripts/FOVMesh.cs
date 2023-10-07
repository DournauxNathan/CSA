using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class FOVMesh : MonoBehaviour
{
    private Mesh mesh;
    public FieldOfView m_fieldofView;

    // Start is called before the first frame update
    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    void Update()
    {
        float fov = m_fieldofView.angle; // Adjust this angle as needed
        Vector3 origin = Vector3.zero;
        int rayCount = m_fieldofView.smoothFactor;
        float angle = 0;
        float angleIncrease = fov / rayCount;
        float fovDistance = m_fieldofView.radius; // Adjust this distance as needed

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
                RaycastHit2D raycasthit2D = Physics2D.Raycast(origin, m_fieldofView.GetVectorFromAngles(angle), fovDistance, m_fieldofView.obstuctionLayer);

                if (raycasthit2D.collider == null)
                {
                    //No hit
                    vertex = origin + m_fieldofView.GetVectorFromAngles(angle) * fovDistance;
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

                trianglesIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrease;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }
}
