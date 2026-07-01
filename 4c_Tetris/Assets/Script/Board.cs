using UnityEngine;
using System.Collections.Generic;

public class Board : MonoBehaviour
{
    [Header("Board Size")]
    public int width = 10;
    public int height = 20;

    [Header("Visuals")]
    [SerializeField] private Sprite blockSprite;

    // grid[x,y] holds the Transform of a placed block, or null
    private Transform[,] grid;

    public void Awake()
    {
        grid = new Transform[width, height];
    }

    public bool IsInside(Vector2Int pos) =>
        pos.x >= 0 && pos.x < width &&
        pos.y >= 0 && pos.y < height;

    public bool IsCellFree(Vector2Int pos) =>
        IsInside(pos) && grid[pos.x, pos.y] == null;

    public bool IsValidPosition(List<Vector2Int> cells)
    {
        foreach (var c in cells)
            if (!IsCellFree(c)) return false;
        return true;
    }

    public void PlaceTetromino(List<Vector2Int> cells, Color color)
    {
        foreach (var c in cells)
        {
            var t = CreateBlock(color);
            t.position = new Vector3(c.x + 0.5f, c.y + 0.5f, 0f);
            grid[c.x, c.y] = t;
        }

        int cleared = ClearLines();
        if (cleared > 0)
            ScoreManager.Instance.AddLines(cleared);
    }

    private Transform CreateBlock(Color color)
    {
        var go = new GameObject("Block");
        var sr = go.AddComponent<SpriteRenderer>();
        sr.sprite = blockSprite;
        sr.color = color;
        return go.transform;
    }

    private void ShiftDown(int fromY)
    {
        for (int y = fromY + 1; y < height; y++)
            for (int x = 0; x < width; x++)
            {
                var t = grid[x, y];
                if (t != null)
                {
                    grid[x, y - 1] = t;
                    grid[x, y] = null;
                    t.position += Vector3.down;
                }
            }
    }

    private int ClearLines()
    {
        int clearedCount = 0;
        for (int y = 0; y < height; y++)
        {
            if (IsLineFull(y))
            {
                ClearLine(y);
                ShiftDown(y);
                y--;
                clearedCount++;
            }
        }
        return clearedCount;
    }

    private bool IsLineFull(int y)
    {
        for (int x = 0; x < width; x++)
            if (grid[x, y] == null) return false;
        return true;
    }

    private void ClearLine(int y)
    {
        for (int x = 0; x < width; x++)
        {
            if (grid[x, y] != null)
            {
                Destroy(grid[x, y].gameObject);
                grid[x, y] = null;
            }
        }
    }
}