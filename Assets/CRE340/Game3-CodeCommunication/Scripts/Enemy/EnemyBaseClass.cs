using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, IDamagable
{
    protected Color originalColor;         // Stores the original color
    private Material mat;                  // Reference to the material

    private void Start()
    {
        // Cache the material and the original color
        mat = GetComponent<Renderer>().material;
        originalColor = mat.color;
    }

    public abstract void TakeDamage(int damage); // Abstract for unique damage handling
    protected abstract void Die();               // Abstract for unique death handling

    public abstract void Move();                 // Optional: unique movement behaviour

    public void ShowHitEffect()
    {
        // Apply hit effect by changing the material color to red
        mat.color = Color.red;
        Invoke("ResetMaterial", 0.1f);           // Reset after a short delay
    }

    protected void ResetMaterial()
    {
        // Reset material color to the original color
        mat.color = originalColor;
    }
}