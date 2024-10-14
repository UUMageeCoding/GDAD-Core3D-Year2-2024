
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    public int health = 10;
    
    private Material mat;
    private Color originalColor;
    
    private void Start(){
        mat = GetComponent<Renderer>().material;
        originalColor = mat.color;
    }
    
    public void TakeDamage(int damage)
    {
        health -= damage;

        // Trigger the OnObjectDamaged event
        HealthEventManager.OnObjectDamaged?.Invoke( health);

        ShowHitEffect();

        if (health <= 0)
        {
            Die();

            // Trigger the OnObjectDestroyed event
            HealthEventManager.OnObjectDestroyed?.Invoke( health);
        }
    }
    
    private void Die()
    {
        // Optional: add death logic, like spawning loot or playing an animation
        Destroy(gameObject);
        //debug log to show that the enemy has died
        Debug.Log("Enemy has died");
    }
    
    public void ShowHitEffect()
    {
        //get the material and flash it red
        Material mat = GetComponent<Renderer>().material;
        mat.color = Color.red;
        Invoke("ResetMaterial", 0.1f);
    }

    private void ResetMaterial(){
        mat.color = originalColor;
    }
}
