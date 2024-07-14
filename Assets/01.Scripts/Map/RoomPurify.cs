using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

struct PurifyData {
    public Vector2Int min;
    public Vector2Int max;
}

public class RoomPurify : RoomBase
{
    [SerializeField] Tilemap mapTemplate;
    [SerializeField] TileBase tile;
    [SerializeField] TileBase failtile;
    [SerializeField] TileBase doortile;

    List<PurifyData> purifies = new();

    public override void SetSize()
    {
        Size = new(30, 30);
    }

    public override void OnComplete()
    {
        base.OnComplete();
        
        Tilemap purifyMap = MapManager.Instance.TileManager.GetMap(TileMapType.Purify);
        if (purifyMap == null)
            purifyMap = MapManager.Instance.TileManager.CreateMap(TileMapType.Purify, mapTemplate);

        int createNum = Random.Range(1, 5);
        int created = 0, failCount = 0;

        while (createNum > created && failCount < 50) {
            Vector2Int size;
            Vector2Int minPos = new(), maxPos;
            bool isStop = false;

            bool width = Random.Range(0, 2) == 1;
            if (width) {
                size = new(Random.Range((int)((Size.x - 2) * 0.1f), Size.x - 1), Random.Range(3, 6));
            } else {
                size = new(Random.Range(3, 6), Random.Range((int)((Size.y - 2) * 0.1f), Size.y - 1));
            }

            // 크기 차단
            if (size.x < 5 && size.y < 5) {
                failCount++;
                continue;
            }
            
            if (size.x >= Size.x - 5 || size.y >= Size.y - 5) {
                failCount++;
                continue;
            }

            minPos.x = Random.Range( MinPos.x + 1, ((MaxPos.x - 1) - size.x) + 1 );
            minPos.y = Random.Range( MinPos.y + 1, ((MaxPos.y - 1) - size.y) + 1 );

            maxPos = minPos + size - Vector2Int.one;

            // 문 가까운지 확인
            int nearSize = 3;
            foreach (var item in doors)
            {
                Vector2Int minPos2 = new();
                Vector2Int maxPos2 = new();

                if (item.Key == MapGenerator.Direction.Top) {
                    minPos2.y = MaxPos.y - nearSize;
                    minPos2.x = item.Value.size.x;
                    
                    maxPos2.y = MaxPos.y;
                    maxPos2.x = item.Value.size.y;
                } else if (item.Key == MapGenerator.Direction.Bottom) {
                    minPos2.y = MinPos.y;
                    minPos2.x = item.Value.size.x;

                    maxPos2.y = MinPos.y + nearSize;
                    maxPos2.x = item.Value.size.y;
                } else if (item.Key == MapGenerator.Direction.Left) {
                    minPos2.y = item.Value.size.x;
                    minPos2.x = MinPos.x;

                    maxPos2.y = item.Value.size.y;
                    maxPos2.x = MinPos.x + nearSize;
                } else if (item.Key == MapGenerator.Direction.Right) {
                    minPos2.y = item.Value.size.x;
                    minPos2.x = MaxPos.x - nearSize;

                    maxPos2.y = item.Value.size.y;
                    maxPos2.x = MaxPos.x;
                }

                if (maxPos2.x < minPos.x || minPos2.x > maxPos.x) continue;
                if (maxPos2.y < minPos.y || minPos2.y > maxPos.y) continue;

                isStop = true;
                break;
            }

            if (isStop) {
                failCount ++;
                continue;
            }

            // 정화구역 만들때 다른 구역 비교해서 구멍이 너무 작을 경우 스탑 처리
            foreach (var item in purifies) {
                // 겹치면 상관없다.
                if (item.max.x >= minPos.x - 1 && item.min.x <= maxPos.x + 1 && item.max.y >= minPos.y - 1 && item.min.y <= maxPos.y + 1) continue;

                // 안겹치는데 ( 3 거리 안에 없음 )
                if (item.max.x < minPos.x - 3 || item.min.x > maxPos.x + 3) continue;
                if (item.max.y < minPos.y - 3 || item.min.y > maxPos.y + 3) continue;

                isStop = true;
                break;
            }

            if (isStop) {
                failCount ++;
                continue;
            }
            

            for (int y = minPos.y; y < minPos.y + size.y; y++)
            {
                for (int x = minPos.x; x < minPos.x + size.x; x++) {
                    purifyMap.SetTile(new Vector3Int(x, y), tile);
                }
            }

            purifies.Add(new() {
                min = minPos,
                max = maxPos
            });

            failCount = 0;
            created ++;
        }
    }
}