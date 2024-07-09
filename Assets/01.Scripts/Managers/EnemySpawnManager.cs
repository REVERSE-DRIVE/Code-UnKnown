using System.Collections.Generic;
using ObjectPooling;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public EnemyBase Enemy;
    public EnemyTableSO enemyTableSO;
    public List<EnemyTableSO> enemyTableSOList;
    
    private EnemyBase SpawnEnemy(Vector3 spawnPosition, PoolingType enemyType)
    {
        var enemy = PoolingManager.Instance.Pop(enemyType) as EnemyBase;
        enemy.transform.position = spawnPosition;
        return enemy;
    }
    
    public EnemyBase SpawnEnemy(Vector3 spawnPosition, int enemyId)
    {
        var enemySO = enemyTableSO.GetEnemyType(enemyId);
        return SpawnEnemy(spawnPosition, enemySO.enemyType);
    }
    
    public void AllEnemySpawn(int index, Vector3 spawnPosition)
    {
        foreach (var enemy in enemyTableSOList[index].enemySOList)
        {
            var enemyBase = PoolingManager.Instance.Pop(enemy.enemyType) as EnemyBase;
            enemyBase.transform.position = spawnPosition;
        }
    }
    
    public void DespawnEnemy(EnemyBase enemy)
    {
        PoolingManager.Instance.Push(enemy);
    }
}