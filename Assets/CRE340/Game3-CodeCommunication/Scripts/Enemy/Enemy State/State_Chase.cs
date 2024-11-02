using UnityEngine;

public class State_Chase : IEnemyState
{
    public void Enter(Enemy enemy)
    {
        Debug.Log("Entering Chase State");
        // Additional setup if needed (e.g., animation)
    }

    public void Update(Enemy enemy)
    {
        if (enemy.target != null)
        {
            // Calculate direction towards the player
            Vector3 direction = (enemy.target.position - enemy.transform.position).normalized;

            // Move the enemy towards the player
            enemy.transform.position += direction * enemy.enemyData.speed * Time.deltaTime;

            // Check if player is out of chase range
            if (Vector3.Distance(enemy.transform.position, enemy.target.position) > enemy.chaseRange)
            {
                enemy.SetState(new State_Idle());
            }
        }
    }

    public void Exit(Enemy enemy)
    {
        Debug.Log("Exiting Chase State");
        // Clean up if needed
    }
}