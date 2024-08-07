using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapBossGenerator
{
    MapGenerator generator;

    public MapBossGenerator() {
        generator = MapManager.Instance.Generator;
    }

    public void CreateBoss(RoomBase room) {
        MapManager.Instance.TearEffect.TearMap(room);
        MapManager.Instance.StartCoroutine(CreateBossRoom(room));
    }

    IEnumerator CreateBossRoom(RoomBase room) {
        yield return new WaitForSeconds(1);


        Vector2Int roomCenter = MapManager.Instance.GetCellByWorldPos(room.GetCenterCoords());
        BossRoomSO option = generator.GetOption().BossOption;
        Vector2Int size = option.Size.GetValue();
        
        Vector2Int minPos = new Vector2Int(roomCenter.x, roomCenter.y) - (size / 2);
        Vector2Int maxPos = new Vector2Int(roomCenter.x, roomCenter.y) + (size / 2);

        generator.CreateWall(minPos, new(minPos.x, maxPos.y));
        generator.CreateWall(minPos, new(maxPos.x, minPos.y));
        generator.CreateWall(new(minPos.x, maxPos.y), maxPos);
        generator.CreateWall(new(maxPos.x, minPos.y), maxPos);

        int divide = 10;
        int i = 0;
        for (int y = minPos.y + 1; y <= maxPos.y - 1; y++)
        {
            for (int x = minPos.x + 1; x <= maxPos.x - 1; x++)
            {
                Vector2Int pos = new(x, y);
                MapManager.Instance.StartCoroutine(DelayTile(pos, option.ground, (i % divide) * 0.05f));
  
                i++;
            }
        }
    
        BossManager.Instance.GenerateBoss(option.BossId, roomCenter);
    }

    IEnumerator DelayTile(Vector2Int pos, TileBase tile, float time) {
        yield return new WaitForSecondsRealtime(time);

        generator.DeleteAll(pos);
        generator.CreateGround(pos, tile);
    }
}
