using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DrawPanelInput : MonoBehaviour
{
    [Header("Paint")]
    [SerializeField] private RectTransform _drawPanel;

    [Header("Mesh")]
    [SerializeField] private MeshGenerator _meshGenerator;

    [Header("Human")]
    [SerializeField] private MeshReplacer _meshReplacer;

    private Painting _painting;

    private bool _isOnDrawField;

    private bool _isPainting;

    private void Awake()
    {
        _painting = new Painting(GetComponent<LineRenderer>(), _drawPanel);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && _isOnDrawField)
        {
            _isPainting = true;

            _painting.Update();
        }
        else if(_isPainting)
        {
            _isPainting = false;

            List<Vector3> _dotsPoint = _painting.Complete();

            DrawMesh(_dotsPoint);
        }
    }

    private void DrawMesh(List<Vector3> dotsPoint)
    {
        if (dotsPoint.Count <= 1) return;

        MeshContainer meshContainer = _meshGenerator.GenerateMesh(dotsPoint);

        _meshReplacer.Replace(meshContainer);
    }

    // Called from DrawField.cs
    public void MouseOnDrawField()
    {
        _isOnDrawField = true;
    }

    public void MouseOutOfDrawField()
    {
        _isOnDrawField = false;
    }
}
