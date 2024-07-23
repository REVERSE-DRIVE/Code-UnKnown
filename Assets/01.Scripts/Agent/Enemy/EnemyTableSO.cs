using System.Collections.Generic;
using ObjectPooling;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/EnemyTableSO", fileName = "EnemyTableSO")]
public class EnemyTableSO : ScriptableObject
{
    public int id;
    public List<EnemyBase> enemyList;
}