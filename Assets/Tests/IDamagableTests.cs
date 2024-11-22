using NUnit.Framework;

public class IDamagableTests
{
    [Test]
    public void TakeDamage_ReducesHealthCorrectly()
    {
        // Arrange
        var damagable = new TestDamagableObject(100);

        // Act
        damagable.TakeDamage(30);

        // Assert
        Assert.AreEqual(70, damagable.Health);
    }

    [Test]
    public void TakeDamage_DoesNotReduceHealthBelowZero()
    {
        // Arrange
        var damagable = new TestDamagableObject(50);

        // Act
        damagable.TakeDamage(100);

        // Assert
        Assert.AreEqual(0, damagable.Health);
    }

    [Test]
    public void ShowHitEffect_SetsEffectTriggered()
    {
        // Arrange
        var damagable = new TestDamagableObject(100);

        // Act
        damagable.ShowHitEffect();

        // Assert
        Assert.IsTrue(damagable.HitEffectTriggered);
    }
    
    [Test]
    public void TakeDamage_WithNegativeValue_DoesNotIncreaseHealth()
    {
        // Arrange
        var damagable = new TestDamagableObject(100);

        // Act
        damagable.TakeDamage(-10);

        // Assert
        Assert.AreEqual(100, damagable.Health);
    }
}