using UnityEngine;
using TMPro;

public class GameManager_Collectathon : MonoBehaviour
{
    public static GameManager_Collectathon instance;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public GameObject winText;
    public GameObject gameOverText;

    private int score = 0;
    private int totalCollectibles;
    public float timer = 20f; // 60=1min

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        totalCollectibles = FindObjectsOfType<Collectible>().Length;
        UpdateScoreText();
        winText.SetActive(false);
        gameOverText.SetActive(false);
    }

    void Update()
    {
        timer -= Time.deltaTime;
        UpdateTimerText();

        if (timer <= 0)
        {
            GameOver();
        }

        if (score >= totalCollectibles)
        {
            Win();
        }
    }

    public void IncreaseScore()
    {
        score++;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score : " + score.ToString();
    }

    void UpdateTimerText()
    {
        timerText.text = "Time : " + Mathf.Ceil(timer).ToString();
    }

    void Win()
    {
        winText.SetActive(true);
        Time.timeScale = 0f;
    }

    void GameOver()
    {
        gameOverText.SetActive(true);
        Time.timeScale = 0f;
    }
}