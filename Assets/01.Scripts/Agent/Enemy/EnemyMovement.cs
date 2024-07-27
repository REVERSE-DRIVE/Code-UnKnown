using UnityEngine;

public class EnemyMovement : AgentMovement
{
    public void LookToTarget(Vector3 targetPosition)
    {
        if (targetPosition.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
    
    public void SetMovement(Vector3 targetPosition)
    {
       
    }
    
    public Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
    }
}
