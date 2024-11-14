using UnityEngine;
using DG.Tweening;

public class CollectableCoin : MonoBehaviour
{
    public GameObject collectEffectPrefab; // Prefab to instantiate on collection
    public int coinValue = 1; // Value of the coin

    private void OnEnable()
    {
        // Scale the item up from 0 to 1 in 1 second using DOTween
        transform.localScale = Vector3.zero;
        transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 1f).SetEase(Ease.OutBounce);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collect();
        }
    }

    public void Collect()
    {
        // Add coins to the GameManager
        GameManager.Instance.AddCoins(coinValue);

        // Instantiate the collect effect prefab at the item's position
        if (collectEffectPrefab != null)
        {
            Instantiate(collectEffectPrefab, transform.position, Quaternion.identity);
        }

        // Play audio feedback when the object is collected
        AudioEventManager.PlaySFX(null, "PowerupUpgrade", 1.0f, 1.0f, true, 0.1f, 0f, "null");

        Collected();
    }

    private void Collected()
    {
        Destroy(gameObject); // Optionally destroy the item in the scene after collection
    }
}