using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
struct RewardRandomData {
    public int amount; // 뽑을꺼
    public PercentGameObject[] list; // 리스트
}

[System.Serializable]
struct PercentGameObject {
    public int percent;
    public GameObject entity;
}

public class RoomReward : RoomBase
{
    [SerializeField] RewardRandomData[] randomData;
    [SerializeField] GameObject prefab;

    public override void OnComplete()
    {
        base.OnComplete();

        print("RandomPosCenter");

        List<GameObject> spawnObjects = new();
        foreach (var item in randomData)
        {
            int[] cumulatives = GetCumulatives(item.list);
            int k, rand;
            
            for (int i = 0; i < item.amount; i++)
            {
                rand = Random.Range(0, 100 + 1);
                k = 0;
                foreach (int cum in cumulatives)
                {
                    if (rand < cum) {
                        spawnObjects.Add(item.list[k].entity);
                        break;
                    }
                    k++;    
                }
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

        print(limit);
        
        Vector3 centerPos = GetCenterCoords();
        Vector3 worldMax = MapManager.Instance.GetWorldPosByCell(MaxPos - Vector2Int.one);
        Vector3 worldMin = MapManager.Instance.GetWorldPosByCell(MinPos + Vector2Int.one);
        
        Vector3 sizeHalf = (worldMax - worldMin) / 2;
        Vector3 pos = centerPos;
        pos.x += Random.Range(0, sizeHalf.x * limit) * (Random.Range(0, 2) == 1 ? 1 :-1);
        pos.y += Random.Range(0, sizeHalf.y * limit) * (Random.Range(0, 2) == 1 ? 1 :-1);
        
        return pos;
    }

    int[] GetCumulatives(PercentGameObject[] list) {
        int[] result = new int[list.Length];

        int i = 0, total = 0;
        foreach (var item in list)
        {
            total += item.percent;
            result[i] = total;
            i++;
        }

        return result;
    }
}
