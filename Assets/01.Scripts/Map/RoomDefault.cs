using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDefault : RoomObstacleBase
{
    [SerializeField] Vector2Int enemyCount;

    bool process = false;
    bool isClear = false;

    List<EnemyBase> enemys;

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
        
        ////// enemy 소환
        enemys = new();

        for (int i = 0; i < Random.Range(enemyCount.x, enemyCount.y); i++)
        {
            var enemy = PoolingManager.Instance.Pop(ObjectPooling.PoolingType.Type_A) as EnemyBase;
            enemys.Add(enemy);

            enemy.transform.position = MapManager.Instance.GetWorldPosByCell(FindPossibleRandomPos(1));
        }
    }

    public override void RoomLeave()
    {
        if (isClear || process) return;

    }
}
