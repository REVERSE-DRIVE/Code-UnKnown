using ObjectPooling;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/EnemySO", fileName = "EnemySO")]
public class EnemySO : ScriptableObject
{
    public int id;
    public PoolingType enemyType;
}