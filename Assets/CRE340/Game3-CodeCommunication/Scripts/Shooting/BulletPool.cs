using UnityEngine;
using System.Collections.Generic;

public class BulletPool : MonoBehaviour
{
    public GameObject bulletPrefab; // The prefab to pool
    public int poolSize = 10; // Number of bullets in the pool

    private Queue<GameObject> pool = new Queue<GameObject>();

    void Start()
    {
        // Prepopulate the pool with bullet instances
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false); // Disable bullets by default
            pool.Enqueue(bullet);
        }
    }

    public GameObject GetBullet()
    {
        // If the pool is empty, create a new bullet (fallback for overflow)
        if (pool.Count == 0)
        {
            GameObject newBullet = Instantiate(bulletPrefab);
            newBullet.SetActive(false);
            return newBullet;
        }

        // Retrieve a bullet from the pool
        GameObject bullet = pool.Dequeue();
        bullet.SetActive(true);
        return bullet;
    }

    public void ReturnBullet(GameObject bullet)
    {
        // Reset bullet properties and return it to the pool
        bullet.SetActive(false);
        pool.Enqueue(bullet);
    }
}