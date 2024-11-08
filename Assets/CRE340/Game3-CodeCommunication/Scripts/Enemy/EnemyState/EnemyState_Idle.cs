using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Idle : IEnemyState
{
    private float patrolProbability = 0.001f; // 0.01% chance each frame to start patrolling

    public void Enter(Enemy enemy)
    {
        Debug.Log("Entering Idle State");
    }

    public void Update(Enemy enemy)
    {
        // Check for player in range to switch to Chase
        if (enemy.target != null && Vector3.Distance(enemy.transform.position, enemy.target.position) < enemy.chaseRange)
        {
            enemy.SetState(new EnemyState_Chase());
            return;
        }

        // Randomly decide to switch to Patrol state
        if (Random.value < patrolProbability)
        {
            enemy.SetState(new EnemyState_Patrol());
        }
    }

    public void Exit(Enemy enemy)
    {
        Debug.Log("Exiting Idle State");
    }
}