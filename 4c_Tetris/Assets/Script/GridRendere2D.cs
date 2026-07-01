using UnityEngine;

public class GridRenderer2D : MonoBehaviour
{
    [Header("Grid Size (cells)")]
    public int width = 10;
    public int height = 20;

    [Header("Appearance")]
    public float cellSize = 1f;
    public float lineWidth = 0.03f;
    public Color lineColor = new Color(1, 1, 1, 0.25f);
    public int sortingOrder = -10;

    void OnEnable() { Rebuild(); }
    void OnValidate() { Rebuild(); }

    public void Rebuild()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            if (Application.isEditor)
                DestroyImmediate(transform.GetChild(i).gameObject);
            else
                Destroy(transform.GetChild(i).gameObject);
        }

        for (int x = 0; x <= width; x++)
        {
            Vector3 a = new Vector3(x * cellSize, 0, 0);
            Vector3 b = new Vector3(x * cellSize, height * cellSize, 0);
            CreateLine(a, b, $"V{x}");
        }

        for (int y = 0; y <= height; y++)
        {
            Vector3 a = new Vector3(0, y * cellSize, 0);
            Vector3 b = new Vector3(width * cellSize, y * cellSize, 0);
            CreateLine(a, b, $"H{y}");
        }
    }

    void CreateLine(Vector3 a, Vector3 b, string name)
    {
        var go = new GameObject(name);
        go.transform.SetParent(transform, false);
        var lr = go.AddComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.useWorldSpace = false;
        lr.SetPosition(0, a);
        lr.SetPosition(1, b);
        lr.startWidth = lr.endWidth = lineWidth;
        lr.startColor = lr.endColor = lineColor;
        lr.numCapVertices = 0;
        lr.numCornerVertices = 0;
        lr.alignment = LineAlignment.View;
        lr.sortingOrder = sortingOrder;
    }
}