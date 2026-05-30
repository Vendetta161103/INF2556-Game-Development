using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject gameOverScreen;
    private int score = 0;

    void Awake()
    {
        if (Instance == null){
            Instance = this;
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score;   // HUD updaten
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;   
        Cursor.visible = true;                     
    }

    public void Restart(){
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}