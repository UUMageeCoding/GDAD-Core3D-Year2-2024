using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Display : MonoBehaviour
{
    public TextMeshProUGUI scoreText;  // Reference to the UI Text element for the score
    public TextMeshProUGUI healthText; // Reference to the UI Text element for health

    // Method to update the score UI
    public void UpdateScore(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    // Method to update the health UI
    public void UpdateHealth(int health)
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + health;
        }
    }
}