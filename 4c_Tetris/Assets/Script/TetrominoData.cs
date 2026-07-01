using UnityEngine;

[System.Serializable]
public struct TetrominoData
{
    public string name; // e.g. "I", "O", "T"
    public Color color;
    public Vector2Int[] cells; // relative block coordinates
    public Vector2Int spawnOffset; // shift from spawn origin

    public TetrominoData(string name, Color color,
        Vector2Int[] cells,
        Vector2Int spawnOffset)
    {
        this.name = name;
        this.color = color;
        this.cells = cells;
        this.spawnOffset = spawnOffset;
    }
}