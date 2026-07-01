using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [SerializeField] private TextMeshProUGUI scoreText;

    private int score = 0;

    // Klassisches Tetris-Scoring: Index = Anzahl gecleateter Reihen (1-4)
    private static readonly int[] LineScores = { 0, 100, 300, 500, 800 };

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        UpdateUI();
    }

    public void AddLines(int lineCount)
    {
        int index = Mathf.Clamp(lineCount, 0, LineScores.Length - 1);
        score += LineScores[index];
        UpdateUI();
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + score;
    }

    public int GetScore() => score;
}