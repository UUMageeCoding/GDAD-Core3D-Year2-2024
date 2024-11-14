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
    public TextMeshProUGUI experienceText;
    public TextMeshProUGUI coinsText;
    
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
    
    // Update the experience in the UI
    public void UpdateExperience(int experience)
    {
        //TODO - add experience UI update
        if(experienceText != null)
        {
            experienceText.text = "Experience: " + experience;
            experienceText.transform.DOShakePosition(1f, 10, 90, 180, false, true);
        }
        
    }
    
    // Update the coins in the UI
    public void UpdateCoins(int coins)
    {
        //TODO - add coins UI update
        if(coinsText != null)
        {
            coinsText.text = "Coins: " + coins;
            coinsText.transform.DOShakeScale(1f, 0.1f, 10, 90, false);
        }
    }
}