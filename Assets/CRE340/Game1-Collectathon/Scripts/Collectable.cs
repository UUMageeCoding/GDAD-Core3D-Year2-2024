using UnityEngine;

public class Collectible : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager_Collectathon.instance.IncreaseScore();
            Destroy(gameObject);
        }
    }
}
