using System.Collections.Generic;
using ObjectPooling;
using UnityEngine;

public class PoolingManager : MonoSingleton<PoolingManager>
{
    private Dictionary<PoolingType, Pool> _pools
        = new Dictionary<PoolingType, Pool>();

    public PoolingTableSO listSO;

    private void Awake()
    {
        Debug.Log(listSO.datas.Count);
        foreach (PoolingItemSO item in listSO.datas)
        {
            Debug.Log($"[ Load Pools ] {item.prefab.type.ToString()}");
            CreatePool(item);
        }
    }

    private void CreatePool(PoolingItemSO item)
    {
        var pool = new Pool(item.prefab, item.prefab.type, transform, item.poolCount);
        _pools.Add(item.prefab.type, pool);
    }

    public IPoolable Pop(PoolingType type)
    {
        if (_pools.ContainsKey(type) == false)
        {
            Debug.LogError($"Prefab dose not exist on Pool : {type}");
            return null;
        }
        
        IPoolable item = _pools[type].Pop();
        item.ResetItem();
        return item;
    }

    public void Push(IPoolable obj, bool resetParent = false)
    {
        if (resetParent)
            obj.ObjectPrefab.transform.SetParent(transform);
        _pools[obj.type].Push(obj);
    }
}
