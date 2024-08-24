using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ObjectPooling;
using UnityEngine;
using UnityEngine.Events;

public class RoomEnemy : RoomBase, IRoomCleable
{
    [SerializeField] Vector2Int enemyCount;
    [SerializeField] RandomPercentUtil<PoolingType>.Value[] enemyList;
    [SerializeField] RewardRandomData[] randomData;
    [SerializeField] MapNearObjectSO nearObjectData;

    bool process = false;
    bool isClear = false;

    List<EnemyBase> enemys;
    List<UnityAction> enemyDieEvents;
    List<MapNearObjectSO.NearObject> nearObjects;

    public void ClearRoomObjects() {}

    public bool IsRoomClear() => isClear;

    public override void OnComplete()
    {
        base.OnComplete();
        
        if (nearObjectData) {
            nearObjects = new();

            foreach (var item in nearObjectData.GetValue())
            {
                Vector3 coords = RandomPosWithNearObject(item.spacing);

                var entity = Instantiate(item.entity, coords, Quaternion.identity);

                nearObjects.Add(new() {
                    entity = entity,
                    spacing = item.spacing
                });
            }
        }
    }

    Vector3 RandomPosWithNearObject(int spacing, int fail = 0) {
        Vector2Int pos = FindPossibleRandomPos(spacing);
        Vector3 coords = MapManager.Instance.GetWorldPosByCell(pos);

        bool result = nearObjects.All(v => {
            return Vector3.Distance(v.entity.transform.position, coords) > ((spacing / 2f) + (v.spacing / 2f));
        });
 
        if (fail <= 50 && !result) {
            return RandomPosWithNearObject(spacing, fail + 1);
        }

        return coords;
    }

    public override void RoomEnter()
    {
        if (isClear || process) return;
        
        process = true;
        SetDoor(true);  
        CameraManager.Instance.HandleZoomCombatMode();
        Transform player = PlayerManager.Instance.player.transform;
        Vector2Int playerPos = MapManager.Instance.GetCellByWorldPos(player.position);

        MapGenerator.Direction doorDir = ClosestDoor(playerPos);
        Vector2Int doorPos = GetCenterPosDoor(doorDir) +  -MapGenerator.GetDirection(doorDir) * 1;

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
        base.RoomLeave();
        if (isClear || process) return;

    }

    void OnEnemyDied(int callbackIdx, EnemyBase enemy) {
        enemy.HealthCompo.OnDieEvent.RemoveListener(enemyDieEvents[callbackIdx]);

        enemys.Remove(enemy);

        if (enemys.Count > 0) return;
        
        // 끝
        isClear = true;
        process = false;
        CameraManager.Instance.HandleZoomNormalMode();

        SetDoor(false);
        
        if (MapManager.Instance.CheckAllClear(true)) return; // 보스 나오는거면 보상 안줌

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