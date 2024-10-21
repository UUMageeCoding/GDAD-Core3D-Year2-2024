using UnityEngine;
using UnityEngine.UI;
using TMPro; // Use TextMeshPro for UI elements

public class UIListener : MonoBehaviour
{
    // Reference to the UI elements
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI playerHealthText;
    public TextMeshProUGUI scoreText;

    private void OnEnable()
    {
        // Subscribe to UI events
        UIEventHandler.OnPlayerNameChanged += UpdatePlayerName;
        UIEventHandler.OnPlayerHealthChanged += UpdatePlayerHealth;
        UIEventHandler.OnScoreChanged += UpdateScore;
    }

    private void OnDisable()
    {
        // Unsubscribe from UI events
        UIEventHandler.OnPlayerNameChanged -= UpdatePlayerName;
        UIEventHandler.OnPlayerHealthChanged -= UpdatePlayerHealth;
        UIEventHandler.OnScoreChanged -= UpdateScore;
    }

    // Update the player name in the UI
    private void UpdatePlayerName(string playerName)
    {
        if (playerNameText != null)
        {
            playerNameText.text = "Player: " + playerName;
        }
    }

    // Update the player health in the UI
    private void UpdatePlayerHealth(int playerHealth)
    {
        if (playerHealthText != null)
        {
            playerHealthText.text = "Health: " + playerHealth;
        }
    }

    // Update the score in the UI
    private void UpdateScore(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}