using UnityEngine;
using DG.Tweening;

public class ExplodingCrate : MonoBehaviour, IDamagable
{
    public int health = 10;
    public GameObject explosionEffectPrefab;

    private Material mat;
    private Color originalColor;

    private void Start()
    {
        mat = GetComponent<Renderer>().material;
        originalColor = mat.color;
    }

    private void OnEnable()
    {
        // TODO - add an animation event to play the spawn animation tween
        //scale the crate up from 0 to 1 in 1 second using DOTween

        
        
    }
    public void TakeDamage(int damage)
    {
        health -= damage;

        // Trigger the OnObjectDamaged event
        HealthEventManager.OnObjectDamaged?.Invoke(gameObject.name, health);

        ShowHitEffect();

        if (health <= 0)
        {
            Explode();

            // Trigger the OnObjectDestroyed event
            HealthEventManager.OnObjectDestroyed?.Invoke(gameObject.name, health);
            Destroy(gameObject);
        }
    }

    public void ShowHitEffect()
    {
        // Flash the material red briefly to show a hit effect
        mat.color = Color.red;
        Invoke(nameof(ResetMaterial), 0.1f);
    }

    private void ResetMaterial()
    {
        mat.color = originalColor;
    }

    private void Explode()
    {
        // Instantiate explosion effect and apply area damage
        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        }
        
        //TODO - add and audio feedback when the crate explodes
        
        
    }
}