using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TetrisGame : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Board board;
    [SerializeField] private Transform blocksRoot;
    [SerializeField] private Sprite blockSprite;

    [Header("Gameplay")]
    [SerializeField] private float fallTime = 0.8f;
    [SerializeField] private float softDropMultiplier = 5f;

    [Header("Difficulty")]
    [SerializeField] private float speedIncreaseInterval = 15f; // alle X Sekunden schneller
    [SerializeField] private float speedIncreaseAmount = 0.05f; // um wie viel fallTime sinkt
    [SerializeField] private float minFallTime = 0.1f;          // untere Grenze

private float difficultyTimer;

    private float fallTimer;
    private float currentFallInterval;
    private Tetromino current;
    private readonly System.Random rng = new System.Random();
    private readonly List<Transform> activeBlocks = new List<Transform>();
    private float moveDir;
    private bool softDropping;

    private void Start()
    {
        currentFallInterval = fallTime;
        SpawnNext();
    }

    private void Update()
    {
        fallTimer += Time.deltaTime;
        if (fallTimer >= currentFallInterval)
        {
            fallTimer = 0f;
            StepDown();
        }

        if (moveDir != 0)
        {
            TryMoveHorizontal((int)Mathf.Sign(moveDir));
            moveDir = 0;
        }

        difficultyTimer += Time.deltaTime;
        if (difficultyTimer >= speedIncreaseInterval)
        {
            difficultyTimer = 0f;
            IncreaseDifficulty();
        }
    }

    private void IncreaseDifficulty()
    {
        fallTime = Mathf.Max(minFallTime, fallTime - speedIncreaseAmount);
        if (!softDropping)
            currentFallInterval = fallTime;
        else
            currentFallInterval = Mathf.Max(0.01f, fallTime / softDropMultiplier);
    }

    private void StepDown()
    {
        if (!current.TryMove(Vector2Int.down))
        {
            LockCurrent();
            if (!SpawnNext()) { ClearAll(); SpawnNext(); }
        }
        RefreshActiveVisual();
    }

    private bool SpawnNext()
    {
        var def = TetrominoLibrary.All[rng.Next(TetrominoLibrary.All.Length)];
        current = new Tetromino(board, def,
            new Vector2Int(board.width / 2, board.height - 2));

        var cells = current.GetCells();
        if (!board.IsValidPosition(cells)) return false;

        CreateActiveVisual(def.color);
        RefreshActiveVisual();
        fallTimer = 0f;
        return true;
    }

    private void LockCurrent()
    {
        board.PlaceTetromino(current.GetCells(), current.data.color);
        DestroyActiveVisual();
    }

    private void CreateActiveVisual(Color color)
    {
        DestroyActiveVisual();
        foreach (var _ in current.data.cells)
        {
            var go = new GameObject("ActiveBlock");
            go.transform.SetParent(blocksRoot != null ? blocksRoot : transform, false);
            var sr = go.AddComponent<SpriteRenderer>();
            sr.sprite = blockSprite;
            sr.color = color;
            activeBlocks.Add(go.transform);
        }
    }

    private void RefreshActiveVisual()
    {
        var cells = current.GetCells();
        for (int i = 0; i < cells.Count; i++)
        {
            var c = cells[i];
            activeBlocks[i].position = new Vector3(c.x + 0.5f, c.y + 0.5f, 0f);
        }
    }

    private void DestroyActiveVisual()
    {
        foreach (var t in activeBlocks) Destroy(t.gameObject);
        activeBlocks.Clear();
    }

    private void TryMoveHorizontal(int dir)
    {
        if (current.TryMove(new Vector2Int(dir, 0)))
            RefreshActiveVisual();
    }

    private void TryRotate()
    {
        if (current.TryRotate(+1))
            RefreshActiveVisual();
    }

    private void DoHardDrop()
    {
        while (current.TryMove(Vector2Int.down)) { }
        LockCurrent();
        if (!SpawnNext()) { ClearAll(); SpawnNext(); }
        RefreshActiveVisual();
    }

    private void ClearAll()
    {
        var root = board != null ? board.transform : transform;
        foreach (Transform child in root) Destroy(child.gameObject);
        DestroyActiveVisual();
        board.Awake();
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) moveDir = Mathf.Clamp(ctx.ReadValue<Vector2>().x, -1f, 1f);
        else if (ctx.canceled) moveDir = 0f;
    }

    public void OnRotate(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) TryRotate();
    }

    public void OnSoftDrop(InputAction.CallbackContext ctx)
    {
        if ((ctx.started || ctx.performed) && !softDropping)
        {
            softDropping = true;
            currentFallInterval = Mathf.Max(0.01f, fallTime / softDropMultiplier);
            fallTimer = 0f;
        }
        else if (ctx.canceled)
        {
            softDropping = false;
            currentFallInterval = fallTime;
        }
    }

    public void OnHardDrop(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) DoHardDrop();
    }

    public void OnPause(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            Time.timeScale = (Time.timeScale < 0.5f) ? 1f : 0f;
    }
}