using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Map/NearObject")]
public class MapNearObjectSO : ScriptableObject
{
    [System.Serializable]
    public struct NearObject {
        public int spacing;
        public GameObject entity;
    }

    [SerializeField] Vector2Int amount;
    [SerializeField] RandomPercentUtil<NearObject>.Value[] list;

    public IEnumerable<NearObject> GetValue() {
        RandomPercentUtil<NearObject> randUtil = new(list);
        
        for (int i = 0; i < Random.Range(amount.x, amount.y + 1); i++)
            yield return randUtil.GetValue();
    }
}
