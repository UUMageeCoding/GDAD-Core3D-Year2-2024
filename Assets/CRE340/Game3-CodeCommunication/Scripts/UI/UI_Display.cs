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
            
            
        }
    }

    // Update the score in the UI
    public void UpdateScore(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
            
            //TODO - add a score animation effect
            
            
            
            //TODO - add a score sound effect
            
            
        }
    }
}