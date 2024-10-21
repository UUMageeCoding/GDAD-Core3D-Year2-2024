using System;
using UnityEngine;

public class UIEventHandler
{
    // Events to notify listeners when player state changes
    public static event Action<string> OnPlayerNameChanged;
    public static event Action<int> OnPlayerHealthChanged;
    public static event Action<int> OnScoreChanged;

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
}