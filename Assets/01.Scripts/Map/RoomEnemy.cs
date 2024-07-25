using System.Collections;
using System.Collections.Generic;
using ObjectPooling;
using UnityEngine;
using UnityEngine.Events;

public class RoomEnemy : RoomBase
{
    [SerializeField] Vector2Int enemyCount;
    [SerializeField] RandomPercentUtil<PoolingType>.Value[] enemyList;
    [SerializeField] RewardRandomData[] randomData;

    bool process = false;
    bool isClear = false;

    List<EnemyBase> enemys;
    List<UnityAction> enemyDieEvents;

    
    public override void SetSize()
    {
        Size = new Vector2Int(Random.Range(30, 50), Random.Range(25, 40));
    }

    public override void RoomEnter()
    {
        if (isClear || process) return;
        
        process = true;
        SetDoor(true);

        Transform player = PlayerManager.Instance.player.transform;
        Vector2Int playerPos = MapManager.Instance.GetCellByWorldPos(player.position);

        MapGenerator.Direction doorDir = ClosestDoor(playerPos);
        Vector2Int doorPos = GetCenterPosDoor(doorDir) +  -MapGenerator.GetDirection(doorDir) * 2;

        player.position = MapManager.Instance.GetWorldPosByCell(doorPos);

        ////// enemy 소환
        enemys = new();
        enemyDieEvents = new();

        RandomPercentUtil<PoolingType> randUtil = new(enemyList);
        for (int i = 0; i < Random.Range(enemyCount.x, enemyCount.y); i++)
        {
            var enemy = PoolingManager.Instance.Pop(randUtil.GetValue()) as EnemyBase;
            enemys.Add(enemy);

            enemy.transform.position = MapManager.Instance.GetWorldPosByCell(FindPossibleRandomPos(1));

            int idx = i;
            enemyDieEvents.Add(() => OnEnemyDied(idx, enemy));

            enemy.HealthCompo.OnDieEvent.AddListener(enemyDieEvents[i]);
        }
    }

    public override void RoomLeave()
    {
        if (isClear || process) return;

    }

    void OnEnemyDied(int callbackIdx, EnemyBase enemy) {
        enemy.HealthCompo.OnDieEvent.RemoveListener(enemyDieEvents[callbackIdx]);

        enemys.Remove(enemy);

        if (enemys.Count > 0) return;
        
        // 끝
        isClear = true;
        process = false;
        
        SetDoor(false);

        // 보상
        foreach (var item in randomData)
        {
            RandomPercentUtil<GameObject> randUtil = new(item.list);
            
            for (int i = 0; i < item.amount; i++)
            {
                GameObject entity = randUtil.GetValue();
                Vector3 pos = MapManager.Instance.GetWorldPosByCell(FindPossibleRandomPos(3));
                Instantiate(entity, pos, Quaternion.identity);
            }
        }
    }
}