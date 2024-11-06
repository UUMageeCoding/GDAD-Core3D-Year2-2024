using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UI_Display : MonoBehaviour
{
    // Reference to the UI elements
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI playerHealthText;
    public TextMeshProUGUI scoreText;
    
    // Update the player name in the UI
    public void UpdatePlayerName(string playerName)
    {
        if (playerNameText != null)
        {
            playerNameText.text = "Player: " + playerName;
        }
    }

    // Update the player health in the UI
    public void UpdatePlayerHealth(int playerHealth)
    {
        if (playerHealthText != null)
        {
            playerHealthText.text = "Health: " + playerHealth;
            
            //TODO - add a health animation effect
            playerHealthText.transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 0.5f, 10, 1f);
        }
    }

    // Update the score in the UI
    public void UpdateScore(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
            
            //TODO - add a score animation effect
            scoreText.transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 0.5f, 10, 1f);
            
            //TODO - add a score sound effect
            AudioEventManager.PlaySFX(null, "UI Beep",  1.0f, 1.0f, true, 0.1f, 0f, "null");
        }
    }
}