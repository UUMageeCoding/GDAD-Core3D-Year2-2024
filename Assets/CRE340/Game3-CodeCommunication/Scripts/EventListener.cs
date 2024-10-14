using UnityEngine;
using TMPro;
public class EventListener : MonoBehaviour
{
    
    public TextMeshProUGUI logText; // Reference to the TextMeshProUGUI component
    
    private void OnEnable()
    {
        // Subscribe to events
        HealthEventManager.OnObjectDamaged += HandleObjectDamaged;
        HealthEventManager.OnObjectDestroyed += HandleObjectDestroyed;
    }

    private void OnDisable()
    {
        // Unsubscribe from events to avoid memory leaks
        HealthEventManager.OnObjectDamaged -= HandleObjectDamaged;
        HealthEventManager.OnObjectDestroyed -= HandleObjectDestroyed;
    }

    private void HandleObjectDamaged(string name, int remainingHealth)
    {
        string message = $"An object called {name} was damaged! Remaining Health: {remainingHealth}";
        Debug.Log(message);
        UpdateLog(message);
    }

    private void HandleObjectDestroyed(string name, int remainingHealth)
    {
        string message = $"An object called {name} was destroyed!";
        Debug.Log(message);
        UpdateLog(message);
    }

    private void UpdateLog(string message)
    {
        if (logText != null)
        {
            logText.text += message + "\n";
        }
    }
}