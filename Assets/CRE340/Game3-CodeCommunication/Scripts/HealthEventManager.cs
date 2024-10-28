using UnityEngine;

// A simple class to define delegates for health-related events
public static class HealthEventManager
{
    
    
    // Define a delegate for damage taken and object destroyed events
    public delegate void HealthEvent(string name, int currentHealth);

    // Multi-delegate: Called when any object implementing IDamagable takes damage
    public static HealthEvent OnObjectDamaged;

    // Multi-delegate: Called when any object implementing IDamagable is destroyed
    public static HealthEvent OnObjectDestroyed;
}