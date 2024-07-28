using System.Collections;
using System.Collections.Generic;
using ObjectPooling;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Map/PhaseSpawnSO")]
public class PhaseSpawnEntitySO : ScriptableObject
{
    public int amount;
    public RandomPercentUtil<PoolingType>.Value[] enemys;
}
