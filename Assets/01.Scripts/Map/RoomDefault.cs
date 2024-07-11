using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomDefault : RoomObstacleBase
{
    [SerializeField] Vector2Int enemyCount;

    bool process = false;
    bool isClear = false;

    List<EnemyBase> enemys;
    List<UnityAction> enemyDieEvents;

    public override void SetSize()
    {
        Size = new Vector2Int(Random.Range(30, 50), Random.Range(25, 40));
    }

    public override void OnComplete()
    {
        base.OnComplete();
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

        for (int i = 0; i < Random.Range(enemyCount.x, enemyCount.y); i++)
        {
            var enemy = PoolingManager.Instance.Pop(ObjectPooling.PoolingType.Type_A) as EnemyBase;
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
    }
}
