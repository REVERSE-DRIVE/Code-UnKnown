using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTracker : RoomBase
{
    [SerializeField] GameObject junkPrefab;
    [SerializeField] HoleObject holePrefab;

    bool isClear = false;

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

        foreach (var item in junkDir)
        {
            var entity = Instantiate(junkPrefab, centerPos + (Vector3)item * spacing, Quaternion.identity);
        }

        HoleSpawn(MinPos, new Vector2(1, 1), MapGenerator.Direction.Bottom); // 왼쪽 아래
        HoleSpawn(new Vector2Int(MinPos.x, MaxPos.y + 1), new Vector2(1, -1), MapGenerator.Direction.Top); // 왼쪽 위
        HoleSpawn(MaxPos + Vector2Int.one, new Vector2(-1, -1), MapGenerator.Direction.Top); // 오른쪽 위
        HoleSpawn(new Vector2Int(MaxPos.x + 1, MinPos.y), new Vector2(-1, 1), MapGenerator.Direction.Bottom); // 오른쪽 아래
    }

    void HoleSpawn(Vector2Int pos, Vector2 spacing, MapGenerator.Direction dir) {
        Vector3 coords = MapManager.Instance.GetWorldPosByCell(pos) + (Vector3)spacing * 3;
        
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

        Instantiate(holePrefab, coords, Quaternion.Euler(0, 0, deg));
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
    }
}
