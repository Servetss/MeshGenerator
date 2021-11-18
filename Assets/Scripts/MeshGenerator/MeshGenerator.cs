using System;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    [Range(2, 10), SerializeField] private int _resolution = 6;

    [Range(0.01f, 0.1f), SerializeField] private float _thickness = 0.05f;

    private Mesh CreateEmptyMesh()
    {
        Mesh mesh = new Mesh();

        mesh.name = "Generated mesh";

        mesh.Clear();

        return mesh;
    }
    
    private void SetVerticesToZeroPosition(Vector3[] points, MeshContainer meshContainer)
    {
        Vector3 zeroPosition = points[0];

        Vector3 highest = Vector3.zero;

        Vector3 lowest = Vector3.zero;

        for (int i = 0; i < points.Length; i++)
        {
            points[i] -= zeroPosition;

            points[i].x = 0;

            if (points[i].y > highest.y)
            {
                highest = points[i];
            }

            if (points[i].y < lowest.y)
            {
                lowest = points[i];
            }
        }

        meshContainer.SetTopVertexes(highest, lowest);
    }
   
    private Vector3[] VerticesGenerete(Vector3[] points, int resolution, float thickness)
    {
        Vector3[] vertices = new Vector3[points.Length * resolution];

        float offset = Mathf.PI / resolution * 2;

        float positionOnCircle = 0;

        float angleRotate = 0;

        int verticesCount = 0;

        for (int i = 0; i < points.Length; i++)
        {
            for (int j = 0; j < resolution; j++)
            {
                if (i > 0)
                {
                    Vector3 direction = (points[i] - points[i - 1]).normalized;

                    float angle = Vector3.SignedAngle(Vector3.up, direction, Vector3.right) * -1;

                    angleRotate = DegreeToRadians(angle);
                }

                float sin = Mathf.Sin(positionOnCircle);

                float cos = Mathf.Cos(positionOnCircle);

                float sinPoint = cos * Mathf.Sin(angleRotate);

                float cosPoint = cos * Mathf.Cos(angleRotate);

                Vector3 vertice = new Vector3(sin * thickness, sinPoint * thickness, cosPoint * thickness) + points[i];

                vertices[verticesCount] = vertice;

                positionOnCircle += offset;

                verticesCount++;

                Color color = Color.black;

                if (j == 0) color = Color.red;
                if (j == 1) color = Color.green;
                if (j == 2) color = Color.blue;
                if (j == 3) color = Color.yellow;

                Debug.DrawLine(points[i], vertice, color, 100);
            }
        }

        return vertices;
    }
    
    private int[] TrianglesGenerete(int pointLength, int resolution)
    {
        int[] triangles = new int[(pointLength - 1) * resolution * 6];

        int trianglesIndex = 0;

        for (int i = 0; i < pointLength - 1; i++)
        {
            for (int j = 0; j < resolution - 1; j++)
            {
                trianglesIndex = 6 * j + (i * resolution * 6);

                triangles[trianglesIndex] = j + (i * resolution);
                triangles[trianglesIndex + 1] = j + (i * resolution) + 1;
                triangles[trianglesIndex + 2] = j + (i * resolution) + resolution;

                triangles[trianglesIndex + 3] = j + (i * resolution) + resolution;
                triangles[trianglesIndex + 4] = j + (i * resolution) + 1;
                triangles[trianglesIndex + 5] = j + (i * resolution) + resolution + 1;
            }

            trianglesIndex = 6 * (resolution - 1) + (i * resolution * 6);

            triangles[trianglesIndex] = (i + 1) * resolution - 1;
            triangles[trianglesIndex + 1] = i * resolution;
            triangles[trianglesIndex + 2] = (i + 2) * resolution - 1;

            triangles[trianglesIndex + 3] = ((i + 2) * resolution) - 1;
            triangles[trianglesIndex + 4] = i * resolution;
            triangles[trianglesIndex + 5] = (i + 1) * resolution;
        }

        return triangles;
    }
    
    private float DegreeToRadians(float angleDegree)
    {
        return angleDegree * Mathf.PI / 180;
    }

    private Mesh FillTheMeshBySplinePoints(Mesh mesh, Vector3[] points)
    {
        mesh.vertices = VerticesGenerete(points, _resolution, _thickness);

        mesh.triangles = TrianglesGenerete(points.Length, _resolution);

        mesh.RecalculateNormals();

        return mesh;
    }

    public MeshContainer GenerateMesh(List<Vector3> points)
    {
        Vector3[] pointsTab = new Vector3[points.Count];

        for (int i = 0; i < pointsTab.Length; i++)
        {
            pointsTab[i] = points[i];
        }

        return GenerateMesh(pointsTab);
    }

    public MeshContainer GenerateMesh(Vector3[] points)
    {
        Mesh mesh = CreateEmptyMesh();

        MeshContainer meshContainer = new MeshContainer();

        SetVerticesToZeroPosition(points, meshContainer);

        meshContainer.SetMesh(FillTheMeshBySplinePoints(mesh, points));

        return meshContainer;
    }
}
