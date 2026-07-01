using UnityEngine;

public static class TetrominoLibrary
{
    public static readonly TetrominoData[] All =
    {
        new TetrominoData("I", new Color(0f, 1f, 1f), new []
        {
            new Vector2Int(-1, 0), new Vector2Int(0, 0),
            new Vector2Int(1, 0), new Vector2Int(2, 0)
        }, Vector2Int.zero),

        new TetrominoData("O", new Color(1f,1f,0f), new[]
        {
            new Vector2Int(0, 0), new Vector2Int(1, 0),
            new Vector2Int(0, 1), new Vector2Int(1, 1)
        }, Vector2Int.zero),

        new TetrominoData("T", new Color(0.6f, 0f, 1f), new[]
        {
            new Vector2Int(-1, 0), new Vector2Int(0, 0),
            new Vector2Int(1, 0), new Vector2Int(0, 1)
        }, Vector2Int.zero),

        new TetrominoData("J", new Color(0f, 0f, 1f), new[]
        {
            new Vector2Int(-1, 1), new Vector2Int(-1, 0),
            new Vector2Int(0, 0), new Vector2Int(1, 0)
        }, Vector2Int.zero),

        new TetrominoData("L", new Color(1f, 0.5f, 0f), new[]
        {
            new Vector2Int(1, 1), new Vector2Int(-1, 0),
            new Vector2Int(0, 0), new Vector2Int(1, 0)
        }, Vector2Int.zero),

        new TetrominoData("S", new Color(0f, 1f, 0f), new[]
        {
            new Vector2Int(-1, 0), new Vector2Int(0, 0),
            new Vector2Int(0, 1), new Vector2Int(1, 1)
        }, Vector2Int.zero),

        new TetrominoData("Z", new Color(1f, 0f, 0f), new[]
        {
            new Vector2Int(-1, 1), new Vector2Int(0, 1),
            new Vector2Int(0, 0), new Vector2Int(1, 0)
        }, Vector2Int.zero)
    };
}