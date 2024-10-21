using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform player; // Reference to the player's transform
    private float speed; // Speed at which the enemy moves

    // Start is called before the first frame update
    void Start()
    {
        // Find the player in the scene
        Invoke("LocatePlayer", 1f);

        // Get the speed from the EnemyData scriptable object
        Enemy enemy = GetComponent<Enemy>();
        if (enemy != null && enemy.enemyData != null)
        {
            speed = enemy.enemyData.speed;
        }
    }

    private void LocatePlayer()
    {
        // Find the player in the scene if the player exaists
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            // Calculate the direction towards the player
            Vector3 direction = (player.position - transform.position).normalized;

            // Move the enemy towards the player
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}