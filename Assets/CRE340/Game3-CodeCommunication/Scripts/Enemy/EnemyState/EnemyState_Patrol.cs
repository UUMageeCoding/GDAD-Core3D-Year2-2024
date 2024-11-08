using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Patrol : IEnemyState
{
    private Vector3 patrolCenter;
    private Vector3 patrolTarget;
    private float patrolRange = 5f;
    private float patrolSpeed = 1.5f;
    private float targetReachedThreshold = 0.2f;
    private float idleProbability = 0.001f; // 0.01% chance each frame to start patrolling

    public void Enter(Enemy enemy)
    {
        Debug.Log("Entering Patrol State");
        patrolCenter = enemy.transform.position;
        SetNewPatrolTarget(enemy);
    }

    public void Update(Enemy enemy)
    {
        // Check for player in range to switch to Chase
        if (enemy.target != null && Vector3.Distance(enemy.transform.position, enemy.target.position) < enemy.chaseRange)
        {
            enemy.SetState(new EnemyState_Chase());
            return;
        }

        // Move towards the current patrol target
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, patrolTarget, patrolSpeed * Time.deltaTime);

        // If close enough to the target, choose a new target
        if (Vector3.Distance(enemy.transform.position, patrolTarget) < targetReachedThreshold)
        {
            SetNewPatrolTarget(enemy);
        }

        // Randomly decide to switch back to Idle state
        if (Random.value < idleProbability)
        {
            enemy.SetState(new EnemyState_Idle());
        }
    }

    public void Exit(Enemy enemy)
    {
        Debug.Log("Exiting Patrol State");
    }

    private void SetNewPatrolTarget(Enemy enemy)
    {
        float randomX = Random.Range(-patrolRange, patrolRange);
        float randomZ = Random.Range(-patrolRange, patrolRange);
        patrolTarget = patrolCenter + new Vector3(randomX, 0, randomZ);
    }
}