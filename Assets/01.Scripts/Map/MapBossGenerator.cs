using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapBossGenerator : MonoBehaviour
{
    [SerializeField] DeadPanel deadPanel;

    MapGenerator generator;
    void Awake() {
        generator = MapManager.Instance.Generator;
    }

    public void CreateBoss(RoomBase room) {
        if (room is IRoomCleable cleable) {
            MapManager.Instance.TearEffect.TearEntitysClear();
            cleable.ClearRoomObjects(); // 삭제하고 뿌수자..
        }
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
    
        // 플레이어 가까이 보내기
        Vector3 minPos3 = MapManager.Instance.GetWorldPosByCell(minPos + Vector2Int.one * 2);
        Vector3 maxPos3 = MapManager.Instance.GetWorldPosByCell(maxPos - Vector2Int.one);

        Transform playerTrm = PlayerManager.Instance.player.transform;
        Vector3 playerPos = playerTrm.position;
        playerTrm.position = new( Mathf.Clamp(playerPos.x, minPos3.x, maxPos3.x), Mathf.Clamp(playerPos.y, minPos3.y, maxPos3.y) );

        BossManager.Instance.GenerateBoss(option.BossId, roomCenter);
        BossManager.Instance.currentBoss.HealthCompo.OnDieEvent.AddListener(OnBossDead);

        var roomSys = Instantiate(option.Room);
        // roomSys.SetRoomPos();...
    }

    IEnumerator DelayTile(Vector2Int pos, TileBase tile, float time) {
        yield return new WaitForSecondsRealtime(time);

        generator.DeleteAll(pos);
        generator.CreateGround(pos, tile);
    }

    void OnBossDead() {
        BossManager.Instance.currentBoss.HealthCompo.OnDieEvent.RemoveListener(OnBossDead);

        Time.timeScale = 0;
        deadPanel.Open();        
    }
}
