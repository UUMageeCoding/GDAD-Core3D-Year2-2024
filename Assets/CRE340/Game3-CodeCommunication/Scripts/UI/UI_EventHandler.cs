using System;
using Unity.VisualScripting;
using UnityEngine;

public class UIEventHandler
{
    // Events to notify listeners when player state changes
    public static event Action<string> OnPlayerNameChanged;
    public static event Action<int> OnPlayerHealthChanged;
    public static event Action<int> OnScoreChanged;
    public static event Action<int> OnExperienceChanged;
    public static event Action<int> OnCoinsChanged;

    // Method to invoke the player name change event
    public static void PlayerNameChanged(string playerName)
    {
        OnPlayerNameChanged?.Invoke(playerName);
    }

    // Method to invoke the player health change event
    public static void PlayerHealthChanged(int playerHealth)
    {
        OnPlayerHealthChanged?.Invoke(playerHealth);
    }

    // Method to invoke the score change event
    public static void ScoreChanged(int score)
    {
        OnScoreChanged?.Invoke(score);
    }
    
    // Method to invoke the experience change event
    public static void ExperienceChanged(int experience)
    {
        OnExperienceChanged?.Invoke(experience);
    }
    
    // Method to invoke the coins change event
    public static void CoinsChanged(int coins)
    {
        OnCoinsChanged?.Invoke(coins);
    }
}