using System;
using System.Collections.Generic;
using ObjectPooling;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemySpawnManager : MonoSingleton<EnemySpawnManager>
{
    public List<EnemyBase> enemyBases;


    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            SpawnEnemy(transform.position, PoolingType.Type_A);
        }
    }

    private EnemyBase SpawnEnemy(Vector3 spawnPosition, PoolingType enemyType)
    {
        var enemy = PoolingManager.Instance.Pop(enemyType) as EnemyBase;
        enemy.transform.position = spawnPosition;
        enemyBases.Add(enemy);
        return enemy;
    }
    
    public void DespawnEnemy(EnemyBase enemy)
    {
        PoolingManager.Instance.Push(enemy);
        enemyBases.Remove(enemy);
    }
}