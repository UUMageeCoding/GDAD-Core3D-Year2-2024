using UnityEngine;

public class EventListener : MonoBehaviour
{
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

    private void HandleObjectDamaged(int remainingHealth)
    {
        Debug.Log($"An object was damaged! Remaining Health: {remainingHealth}");
    }

    private void HandleObjectDestroyed(int remainingHealth)
    {
        Debug.Log("An object was destroyed!");
    }
}