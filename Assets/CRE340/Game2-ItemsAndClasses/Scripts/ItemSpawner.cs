using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject healthPotionPrefab;  // Drag HealthPotion prefab here in the Inspector
    public GameObject manaPotionPrefab;    // Drag ManaPotion prefab here in the Inspector

    public int numberOfItemsEachSide = 3;  // Number of items on each side of the origin (total of 6 items)
    public float spacing = 2.0f;           // Distance between each item along the X-axis

    void Start()
    {
        SpawnHealthPotions();
        SpawnManaPotions();
    }

    // Spawns HealthPotion instances along the X-axis, centered around the origin
    void SpawnHealthPotions()
    {
        for (int i = -numberOfItemsEachSide; i <= numberOfItemsEachSide; i++)
        {
            // Skip the origin (0) for HealthPotion if desired, but it's included here
            Vector3 position = new Vector3(i * spacing, -0.5f, 0);

            // Instantiate the HealthPotion prefab at the calculated position
            GameObject newHealthPotion = Instantiate(healthPotionPrefab, position, Quaternion.identity);

            // Display item info (optional)
            HealthPotion healthPotionItem = newHealthPotion.GetComponent<HealthPotion>();
            if (healthPotionItem != null)
            {
                healthPotionItem.DisplayInfo();
            }
            else
            {
                Debug.LogWarning("The instantiated health potion does not have the HealthPotion component!");
            }
        }
    }

    // Spawns ManaPotion instances along the X-axis, centered around the origin
    void SpawnManaPotions()
    {
        for (int i = -numberOfItemsEachSide; i <= numberOfItemsEachSide; i++)
        {
            // Offset the Y position to differentiate between HealthPotions and ManaPotions
            Vector3 position = new Vector3(i * spacing, -0.5f, 2.0f);  // Z position is set to 2.0 to avoid overlap

            // Instantiate the ManaPotion prefab at the calculated position
            GameObject newManaPotion = Instantiate(manaPotionPrefab, position, Quaternion.identity);

            // Display item info (optional)
            ManaPotion manaPotionItem = newManaPotion.GetComponent<ManaPotion>();
            if (manaPotionItem != null)
            {
                manaPotionItem.DisplayInfo();
            }
            else
            {
                Debug.LogWarning("The instantiated mana potion does not have the ManaPotion component!");
            }
        }
    }
}
