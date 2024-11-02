using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Chase : IEnemyState
{
    public void Enter(Enemy enemy)
    {
        Debug.Log("Entering Chase State");
        // Additional setup if needed (e.g., animation)
    }

    public void Update(Enemy enemy)
    {
        // Move towards the player
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, enemy.target.position, enemy.speed * Time.deltaTime);

        // Transition back to Idle if player is out of range
        if (Vector3.Distance(enemy.transform.position, enemy.target.position) > enemy.chaseRange)
        {
            enemy.SetState(new EnemyState_Idle());
        }
    }

    public void Exit(Enemy enemy)
    {
        Debug.Log("Exiting Chase State");
        // Clean up if needed
    }
}