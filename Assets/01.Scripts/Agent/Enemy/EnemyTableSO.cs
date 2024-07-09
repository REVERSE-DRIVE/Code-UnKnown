using System.Collections.Generic;
using ObjectPooling;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/EnemyTableSO", fileName = "EnemyTableSO")]
public class EnemyTableSO : ScriptableObject
{
    public int id;
    public List<EnemySO> enemySOList;

    
    
    public EnemySO GetRandomEnemyType()
    {
        return enemySOList[Random.Range(0, enemySOList.Count)];
    }
    
    public EnemySO GetEnemyType(int id)
    {
        return enemySOList.Find(enemySO => enemySO.id == id);
    }
    
    public EnemySO GetEnemyType(PoolingType type)
    {
        return enemySOList.Find(enemySO => enemySO.enemyType == type);
    }

    public EnemySO GetEnemyTypeByIndex(int index)
    {
        return enemySOList[index];
    }
}