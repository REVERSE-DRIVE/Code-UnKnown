using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapTearEffect : MonoBehaviour
{
    [SerializeField] Vector2Int sliceCut = new(3, 3); // 방을 3등분 함
    [SerializeField] GameObject debugTile;
    [SerializeField] TileBase red;
    [SerializeField] TileBase green;
    [SerializeField] TileBase blue;
    [SerializeField] TileBase yellow;
    
    List<GameObject> tearEntitys;

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

        Vector3 centerPos = room.GetCenterCoords();

        // 타일 분리
        GameObject[,] tilemaps = new GameObject[sliceCut.y, sliceCut.x];
        for (int i = 0; i < sliceCut.y; i++)
            for (int v = 0; v < sliceCut.x; v++) {
                GameObject tileEntity = tilemaps[i, v] = Instantiate(debugTile, MapManager.Instance.TileManager.GetRoot());
                Tilemap tilemap = tileEntity.GetComponentInChildren<Tilemap>();
                
                // 복제( 와 동시에 기존꺼 삭제 )
                foreach (var pos in groups[i, v])
                {
                    var tilebase = MapManager.Instance.Generator.GetTileBaseByCoords(pos);
                    tilemap.SetTile((Vector3Int)pos, tilebase);
                    MapManager.Instance.Generator.DeleteAll(pos);
                }

                // 날리기
                
                // 중간 값 구함
                Vector2Int min = groups[i, v][0];
                Vector2Int max = groups[i, v][0];

                foreach (var item in groups[i, v]) {
                    // if (item.x < min.x || item.y < min.y) min = item;
                    // if (item.x > max.x || item.y > max.y) max = item;

                    if (item.x < min.x) min.x = item.x;
                    if (item.y < min.y) min.y = item.y;

                    if (item.x > max.x) max.x = item.x;
                    if (item.y > max.y) max.y = item.y;
                }

                Vector3 groupCenter = tilemap.CellToWorld((Vector3Int)(min + max) / 2);
                Vector3 direction = (groupCenter - centerPos).normalized;

                tilemap.transform.position -= groupCenter;
                tileEntity.transform.position += groupCenter;
                tileEntity.GetComponent<TilemapTear>().RegisterThrowTile(direction, 10);
            }

        if (tearEntitys != null)
            foreach (var item in tearEntitys)
            {
                bool hasRigid = item.TryGetComponent<Rigidbody2D>(out Rigidbody2D rigid);
                if (!hasRigid)
                    rigid = item.AddComponent<Rigidbody2D>();
                    
                
            }
    }
    
    public void TearEntitysClear() {
        tearEntitys = new();
    }
    public void RegisterTearObject(GameObject entity) {
        tearEntitys.Add(entity);
    }
}
