using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Idle : IEnemyState
{
    public void Enter(Enemy enemy)
    {
        Debug.Log("Entering Idle State");
        // Additional setup if needed (e.g., animation)
    }

    public void Update(Enemy enemy)
    {
        if(enemy.target == null)
        {
            return;
        }
        // Simple transition condition: switch to Chase if player is within a certain distance
        if (Vector3.Distance(enemy.transform.position, enemy.target.position) < enemy.chaseRange)
        {
            enemy.SetState(new EnemyState_Chase());
        }
    }

    public void Exit(Enemy enemy)
    {
        Debug.Log("Exiting Idle State");
        // Clean up if needed
    }
}
