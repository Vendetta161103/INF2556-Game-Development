using System.Collections.Generic;
using UnityEngine;

public class Tetromino
{
    public TetrominoData data;

    private readonly Board board;
    private Vector2Int origin;
    private List<Vector2Int> relativeCells;

    public Tetromino(Board board, TetrominoData data, Vector2Int origin)
    {
        this.board = board;
        this.data = data;
        this.origin = origin;

        relativeCells = new List<Vector2Int>();
        foreach (var c in data.cells)
            relativeCells.Add(c);
    }

    public List<Vector2Int> GetCells()
    {
        var result = new List<Vector2Int>();
        foreach (var c in relativeCells)
            result.Add(origin + c);
        return result;
    }

    public bool TryMove(Vector2Int delta)
    {
        var newOrigin = origin + delta;
        var newCells = new List<Vector2Int>();
        foreach (var c in relativeCells)
            newCells.Add(newOrigin + c);

        if (!board.IsValidPosition(newCells))
            return false;

        origin = newOrigin;
        return true;
    }

    public bool TryRotate(int dir)
    {
        var rotated = new List<Vector2Int>();
        foreach (var c in relativeCells)
        {
            Vector2Int newCell = dir >= 0
                ? new Vector2Int(-c.y, c.x)   // clockwise: (x,y) -> (-y,x)
                : new Vector2Int(c.y, -c.x);  // counter-clockwise
            rotated.Add(newCell);
        }

        var newCells = new List<Vector2Int>();
        foreach (var c in rotated)
            newCells.Add(origin + c);

        if (!board.IsValidPosition(newCells))
            return false;

        relativeCells = rotated;
        return true;
    }
}