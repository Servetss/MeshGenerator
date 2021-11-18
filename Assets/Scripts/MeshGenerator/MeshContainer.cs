using UnityEngine;

public class MeshContainer
{
    public Mesh Mesh { get; private set; }

    public Vector3 HighestVertices { get; private set; }

    public Vector3 LowestVertices { get; private set; }

    public void SetMesh(Mesh mesh)
    {
        Mesh = mesh;
    }

    public void SetTopVertexes(Vector3 highestVertices, Vector3 lowestVertices)
    {
        HighestVertices = highestVertices;

        LowestVertices = lowestVertices;
    }

}
