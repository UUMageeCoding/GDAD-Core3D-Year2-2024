using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, IDamagable
{
    protected Color originalColor;   // Stores the original color
    private Material mat;            // Reference to the material

    private void Awake()
    {
        InitializeMaterial();
    }

    private void InitializeMaterial()
    {
        // Get and cache the material from the Renderer
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            mat = renderer.material;
            originalColor = mat.color;
        }
        else
        {
            Debug.LogError("Renderer component not found on enemy! Ensure a material is assigned.");
        }
    }

    public abstract void TakeDamage(int damage);  // Abstract for unique damage handling
    protected abstract void Die();                // Abstract for unique death handling
    public abstract void Move();                  // Optional: unique movement behaviour

    public void ShowHitEffect()
    {
        // Recheck and initialize mat if it’s unexpectedly null
        if (mat == null) InitializeMaterial();

        if (mat != null)
        {
            mat.color = Color.red;            // Change color to red
            Invoke("ResetMaterial", 0.1f);    // Reset after a short delay
        }
        else
        {
            Debug.LogWarning("Material not found. Hit effect could not be applied.");
        }
    }

    protected void ResetMaterial()
    {
        if (mat != null)
        {
            mat.color = originalColor;        // Reset to the original color
        }
    }
}