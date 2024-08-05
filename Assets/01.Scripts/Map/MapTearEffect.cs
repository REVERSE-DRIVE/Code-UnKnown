using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapTearEffect : MonoBehaviour
{
    [SerializeField] Vector2Int sliceCut = new(3, 3); // 방을 3등분 함
    [SerializeField] Tilemap debugTile;
    [SerializeField] TileBase red;
    [SerializeField] TileBase green;
    [SerializeField] TileBase blue;
    [SerializeField] TileBase yellow;

    public void TearMap(RoomBase room) {
        // X 정하기 (Y 정하기라면 반대)
        /* 예시
            [0] = {
                [10]: 20 (key가 Y value는 X)
            }
        */
        Dictionary<int, int>[] xCuts = new Dictionary<int, int>[sliceCut.x];
        Dictionary<int, int>[] yCuts = new Dictionary<int, int>[sliceCut.y];
        
        Vector2Int splitSize = new Vector2Int(room.Size.x / sliceCut.x, room.Size.y / sliceCut.y);

        // 나눔
        for (int i = 0; i < sliceCut.x - 1; i++)
        {
            xCuts[i] = new();

            for (int y = room.MinPos.y; y <= room.MaxPos.y; y++)
            {
                int startPos = room.MinPos.x + (splitSize.x * i);
                xCuts[i][y] = Random.Range(startPos + splitSize.x - 2, startPos + splitSize.x + 1);
            }
        }

        for (int i = 0; i < sliceCut.y - 1; i++) {
            yCuts[i] = new();
            
            for (int x = room.MinPos.x; x <= room.MaxPos.x; x++)
            {
                int startPos = room.MinPos.y + (splitSize.y * i);
                yCuts[i][x] = Random.Range(startPos + splitSize.y - 2, startPos + splitSize.y + 1);
            }
        }
    
        // 조각으로 묶음
        List<Vector2Int>[,] groups = new List<Vector2Int>[sliceCut.y, sliceCut.x];
        for (int i = 0; i < sliceCut.y; i++)
            for (int v = 0; v < sliceCut.x; v++)
                groups[i, v] = new();
            
        for (int y = room.MinPos.y; y <= room.MaxPos.y; y++) {
            for (int x = room.MinPos.x; x <= room.MaxPos.x; x++) {
                Vector2Int count = sliceCut - Vector2Int.one;
                
                for (int i = 0; i < sliceCut.x - 1; i++) {
                    if (xCuts[i][y] >= x) count.x --;
                    if (yCuts[i][x] >= y) count.y --;
                }

                groups[count.y, count.x].Add(new Vector2Int(x, y));
            }
        }

        // 타일 분리
        Tilemap[,] tilemaps = new Tilemap[sliceCut.y, sliceCut.x];
        for (int i = 0; i < sliceCut.y; i++)
            for (int v = 0; v < sliceCut.x; v++) {
                var tilemap = tilemaps[i, v] = MapManager.Instance.TileManager.CreateMap(TileMapType.TearEffect, debugTile, true);
                
                // 복제( 와 동시에 기존꺼 삭제 )
                foreach (var pos in groups[i, v])
                {
                    var tilebase = MapManager.Instance.Generator.GetTileBaseByCoords(pos);
                    tilemap.SetTile((Vector3Int)pos, tilebase);
                }
            }
    
        
    }
}
