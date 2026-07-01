using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Slider scoreBar;
    [SerializeField] int maxScore = 10;
    [SerializeField] GameObject gameOverPanel;

    private int score = 0;
    private bool isGameOver = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        if (scoreBar != null)
        {
            scoreBar.minValue = 0;
            scoreBar.maxValue = maxScore;
        }
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        UpdateUI();
    }

    public void AddScore(int amount)
    {
        if (isGameOver) return;

        score += amount;
        UpdateUI();

        if (score < 0)
            TriggerLoseState();
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + score;
        if (scoreBar != null)
            scoreBar.value = score;
    }

    void TriggerLoseState()
    {
        isGameOver = true;
        Time.timeScale = 0f;
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}