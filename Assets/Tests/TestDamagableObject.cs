public class TestDamagableObject : IDamagable
{
    public int Health { get; private set; }
    public bool HitEffectTriggered { get; private set; }

    public TestDamagableObject(int initialHealth)
    {
        Health = initialHealth;
        HitEffectTriggered = false;
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0)
            return; // Ignore negative damage

        Health -= damage;
        if (Health < 0) Health = 0;
    }

    public void ShowHitEffect()
    {
        HitEffectTriggered = true; // Simulate visual effect trigger
    }
    
}