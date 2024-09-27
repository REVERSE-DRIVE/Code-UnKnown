using System.Collections.Generic;
using System.Linq;
using ObjectPooling;
using UnityEngine;

public class RoomTracker : RoomBase, IRoomCleable
{
    [SerializeField] JunkFileObject junkPrefab;
    [SerializeField] HoleObject holePrefab;
    [SerializeField] int clearTime = 60;
    [SerializeField] float holeSpace = 3f;

    List<HoleObject> holes = new List<HoleObject>();
    List<JunkFileObject> junks;

    bool isClear = false;

    private void OnDestroy()
    {
        foreach (HoleObject hole in holes)
        {
            if(hole == null)
                continue;
            Destroy(hole.gameObject);
        }
        holes.Clear();
    }

    public override void OnComplete()
    {
        base.OnComplete();
        
        Vector3 centerPos = GetCenterCoords();

        // 정크파일 소환
        Vector2[] junkDir = new Vector2[] {
            new(1, 1),
            new(1, -1),
            new(-1, -1),
            new(-1, 1),
        };
        float spacing = 1; // 간격

        junks = new();
        foreach (var item in junkDir)
        {
            var entity = PoolingManager.Instance.Pop(PoolingType.JunkFileObject) as JunkFileObject;
            entity.transform.position = centerPos + (Vector3)item * spacing;
            junks.Add(entity);

            entity.RestoreHealth(99999);
            entity.GetComponent<Rigidbody2D>().mass = 3;
        }

        holes = new();

        HoleSpawn(MinPos, new Vector2(1, 1), MapGenerator.Direction.Bottom); // 왼쪽 아래
        HoleSpawn(new Vector2Int(MinPos.x, MaxPos.y + 1), new Vector2(1, -1), MapGenerator.Direction.Top); // 왼쪽 위
        HoleSpawn(MaxPos + Vector2Int.one, new Vector2(-1, -1), MapGenerator.Direction.Top); // 오른쪽 위
        HoleSpawn(new Vector2Int(MaxPos.x + 1, MinPos.y), new Vector2(-1, 1), MapGenerator.Direction.Bottom); // 오른쪽 아래

        holes.ForEach(v => v.InEvent += HoleClear);
    }

    void HoleSpawn(Vector2Int pos, Vector2 spacing, MapGenerator.Direction dir) {
        Vector3 coords = MapManager.Instance.GetWorldPosByCell(pos) + (Vector3)spacing * holeSpace;
        
        float deg = 0;
        switch (dir)
        {
            case MapGenerator.Direction.Top:
                deg = 0;
                break;
            case MapGenerator.Direction.Bottom:
                deg = 180;
                break;
            default:
                break;
        }

        var hole = Instantiate(holePrefab, coords, Quaternion.Euler(0, 0, deg));
        holes.Add(hole);
    }

    void HoleClear(HoleObject hole) {
        // 끝났는지 확인
        if (isClear || !holes.All(v => v.GetInEntity() != null)) return;

        // 끝남
        TimerManager.Instance.CancelTimer();
        OnClear(true);
    }

    public override void RoomEnter()
    {
        base.RoomEnter();
        
        if (isClear) return;

        Transform player = PlayerManager.Instance.player.transform;
        Vector2Int playerPos = MapManager.Instance.GetCellByWorldPos(player.position);

        MapGenerator.Direction doorDir = ClosestDoor(playerPos);
        Vector2Int doorPos = GetCenterPosDoor(doorDir) +  -MapGenerator.GetDirection(doorDir) * 2;

        player.position = MapManager.Instance.GetWorldPosByCell(doorPos);

        SetDoor(true);

        TimerManager.Instance.ShowTimer(clearTime);
        TimerManager.Instance.OnFinish += OnTimeEnd;
    }

    void OnTimeEnd() { // 의뢰 실패
        OnClear();
        
        //////// 의뢰 완성도 감소
        // ...
    }

    void OnClear(bool success = false) {
        isClear = true;
        SetDoor(false);
        holes.ForEach(v => v.InEvent -= HoleClear);

        MapManager.Instance.CheckAllClear(success);
    }

    public bool IsRoomClear() => isClear;

    public void ClearRoomObjects(bool force)
    {
        if (force) {
            holes.ForEach(v => {
                if (v != null)
                    Destroy(v.gameObject);
            });
        } else {
            holes.ForEach(v => {
                v.enabled = false;
                MapManager.Instance.TearEffect.RegisterTearObject(v.gameObject);
            });
        }
        junks.ForEach(v => PoolingManager.Instance.Push(v));

        // holes.ForEach(v => Destroy(v.gameObject));
        // junks.ForEach(v => Destroy(v.gameObject));
    }
}
