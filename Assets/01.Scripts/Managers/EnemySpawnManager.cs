using ObjectPooling;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public EnemyBase Enemy;
    
    public EnemyBase SpawnEnemy(Vector3 spawnPosition, PoolingType enemyType)
    {
        var enemy = PoolingManager.Instance.Pop(enemyType) as EnemyBase;
        return enemy;
    }
    
    public void DespawnEnemy(EnemyBase enemy)
    {
        PoolingManager.Instance.Push(enemy);
    }
}