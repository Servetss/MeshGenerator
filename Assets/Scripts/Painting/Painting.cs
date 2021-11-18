
using System.Collections.Generic;
using UnityEngine;

public class Painting
{
    private const int PointsDistanceToDraw = 20;

    private LineRenderer _lineRenderer;

    private RectTransform _drawPanel;

    private Camera _camera;

    private List<Vector3> _dotsDraw;

    private Vector2 _mouseOnPointPoint;

    private int _lineRendererIndex;

    private float _cameraClipPlane; 

    public Painting(LineRenderer lineRenderer, RectTransform drawPanel)
    {
        _camera = Camera.main;

        _lineRenderer = lineRenderer;

        _drawPanel = drawPanel;

        _dotsDraw = new List<Vector3>();

        _cameraClipPlane = _camera.nearClipPlane * 5;
    }

    public void Update()
    {
        if (Vector2.Distance(_mouseOnPointPoint, Input.mousePosition) > PointsDistanceToDraw)
        {
            _mouseOnPointPoint = Input.mousePosition;

            Vector3 pointInWorld = _camera.ScreenToWorldPoint(new Vector3(_mouseOnPointPoint.x, _mouseOnPointPoint.y, _cameraClipPlane));

            _dotsDraw.Add(pointInWorld);

            _lineRenderer.SetVertexCount(_lineRendererIndex + 1);

            _lineRenderer.SetPosition(_lineRendererIndex, pointInWorld);

            _lineRendererIndex++;
        }
    }

    public bool GetVectorOutOfBorder(Vector2 vector)
    {
        return vector.x > Mathf.Abs(_drawPanel.rect.x / 2) || vector.y > Mathf.Abs(_drawPanel.rect.y / 2);
    }

    public List<Vector3> Complete()
    {
        List<Vector3> dots = _dotsDraw;

        _dotsDraw = new List<Vector3>();

        _lineRendererIndex = 0;

        _lineRenderer.SetVertexCount(0);

        return dots;
    }
}
