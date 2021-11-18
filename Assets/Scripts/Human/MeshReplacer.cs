using UnityEngine;

public class MeshReplacer: MonoBehaviour
{
    [SerializeField] private Transform[] _meshesTransform;

    private MeshFilter[] _meshFiltres;

    private MeshCollider[] _meshColiders;

    private void Awake()
    {
        _meshFiltres = new MeshFilter[_meshesTransform.Length];

        _meshColiders = new MeshCollider[_meshesTransform.Length];

        for (int i = 0; i < _meshesTransform.Length; i++)
        {
            _meshFiltres[i] = _meshesTransform[i].GetComponent<MeshFilter>();

            _meshColiders[i] = _meshesTransform[i].GetComponent<MeshCollider>();
        }
    }

    public void Replace(MeshContainer meshContainer)
    {
        for (int i = 0; i < _meshesTransform.Length; i++)
        {
            _meshFiltres[i].mesh = meshContainer.Mesh;

            _meshColiders[i].sharedMesh = meshContainer.Mesh;

            _meshesTransform[i].transform.localPosition = Vector3.zero;

            _meshesTransform[i].transform.position -= meshContainer.HighestVertices;
        }

        float y = Mathf.Abs(meshContainer.HighestVertices.y - meshContainer.LowestVertices.y);

        transform.position = new Vector3(transform.position.x, transform.position.y + y, transform.position.z);
    }
}
