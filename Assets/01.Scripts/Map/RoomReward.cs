using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct RewardRandomData {
    public int amount; // 뽑을꺼
    public RandomPercentUtil<GameObject>.Value[] list; // 리스트
}

public class RoomReward : RoomBase
{
    [SerializeField] RewardRandomData[] randomData;
    [SerializeField] GameObject prefab;

    public override void OnComplete()
    {
        base.OnComplete();

        List<GameObject> spawnObjects = new();
        foreach (var item in randomData)
        {
            RandomPercentUtil<GameObject> randUtil = new(item.list);
            
            for (int i = 0; i < item.amount; i++)
            {
                GameObject entity = randUtil.GetValue();
                spawnObjects.Add(entity);
            }
        }

        for (int i = 0; i < spawnObjects.Count; i++)
        {
            Vector3 pos = RandomPosCenter((1f / spawnObjects.Count) * (i + 1));
            Instantiate(spawnObjects[i], pos, Quaternion.identity);
        }

    }

    // limit 최대 제한 0 ~ 1
    Vector3 RandomPosCenter(float limit) {
        limit = Mathf.Clamp(limit, 0, 1);

        Vector3 centerPos = GetCenterCoords();
        Vector3 worldMax = MapManager.Instance.GetWorldPosByCell(MaxPos - Vector2Int.one);
        Vector3 worldMin = MapManager.Instance.GetWorldPosByCell(MinPos + Vector2Int.one);
        
        Vector3 sizeHalf = (worldMax - worldMin) / 2;
        Vector3 pos = centerPos;
        pos.x += Random.Range(0, sizeHalf.x * limit) * (Random.Range(0, 2) == 1 ? 1 :-1);
        pos.y += Random.Range(0, sizeHalf.y * limit) * (Random.Range(0, 2) == 1 ? 1 :-1);
        
        return pos;
    }
}
