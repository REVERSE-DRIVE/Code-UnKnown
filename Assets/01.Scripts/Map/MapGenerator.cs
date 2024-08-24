using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public struct BridgeBase {
    public Vector2Int start;
    public Vector2Int end;
    
    public RoomBase room1;
    public RoomBase room2;

    public readonly RoomBase GetOtherRoom(RoomBase room) {
        if (room1 != room) return room1;
        else return room2;
    }
}

public class MapGenerator : MonoBehaviour
{
    RoomBase[] createRooms;
    [SerializeField] MapGenerateSO option;
    [field: SerializeField] public MapBossGenerator BossGenerator { get; private set; }
    int nowCreateIdx = -1;

    [SerializeField] Tilemap wallTile;
    [SerializeField] Tilemap bridgeTile;
    [SerializeField] Tilemap groundTile;
    [SerializeField] Tilemap doorTile;

    [SerializeField] TileBase wallBase;
    [SerializeField] TileBase bridgeBase;
    [SerializeField] TileBase doorBase;

    Vector2Int bridgeSize;

    
    // List<RoomBase> rooms = new();
    

    public enum Direction {
        Top,
        Left,
        Right,
        Bottom
    }

    private void Awake() {
        // Generate();
        // BoxGenerate(Direction.Top, null);
        BossGenerator = new();
    }

    float time = 0;
    int functionCall;

    public void StartGenerate() {
        createRooms = option.GetRandomRooms();
        bridgeSize = option.BridgeSize;

        nowCreateIdx = -1;
        functionCall = 0;
        time = Time.time;

        BoxGenerate(Direction.Top, null);

        print($"[MapGenerator] 맵 생성 완료. (호출횟수: {functionCall} / 걸린시간: {Time.time - time})");
    }

    public void StartGenerate(RoomBase[] templates) {
        createRooms = templates;
        StartGenerate();
    }
    
    public void Clear() {
        wallTile.ClearAllTiles();
        bridgeTile.ClearAllTiles();
        groundTile.ClearAllTiles();
    }

    void Generate() {
        bool first = true;
        Direction dir = Direction.Top;
        Vector2Int bridgeEnd = new Vector2Int(0,0);

        Vector2Int nowMapCoords = new Vector2Int(0,0);

        foreach (var item in createRooms)
        {
            var room = Instantiate(item);
            room.SetSize();

            if (first) {
                first = false;
            } else {
                nowMapCoords += GetDirection(dir);
            }
            MapManager.Instance.SetRoom(nowMapCoords, room);
            print(nowMapCoords);


            Vector2Int minPos = Vector2Int.zero;
            Vector2Int maxPos = Vector2Int.zero;

            if (dir == Direction.Top) {
                minPos = new Vector2Int(bridgeEnd.x - (room.Size.x / 2), bridgeEnd.y + 1);
                maxPos = new Vector2Int(bridgeEnd.x + (room.Size.x / 2), bridgeEnd.y + room.Size.y);
            } else if (dir == Direction.Bottom) {
                minPos = new Vector2Int(bridgeEnd.x - (room.Size.x / 2), bridgeEnd.y - room.Size.y);
                maxPos = new Vector2Int(bridgeEnd.x + (room.Size.x / 2), bridgeEnd.y - 1);
            } else if (dir == Direction.Right) {
                minPos = new Vector2Int(bridgeEnd.x + 1, bridgeEnd.y - (room.Size.y / 2));
                maxPos = new Vector2Int(bridgeEnd.x + room.Size.x, bridgeEnd.y + (room.Size.y / 2));
            } else if (dir == Direction.Left) {
                minPos = new Vector2Int(bridgeEnd.x - room.Size.x, bridgeEnd.y - (room.Size.y / 2));
                maxPos = new Vector2Int(bridgeEnd.x - 1, bridgeEnd.y + (room.Size.y / 2));
            }

            for (int i = minPos.y; i <= maxPos.y; i++)
            {
                for (int k = minPos.x; k <= maxPos.x; k++)
                {
                    if (i == minPos.y || i == maxPos.y || k == minPos.x || k == maxPos.x) {
                        wallTile.SetTile(new Vector3Int(k,i,0), wallBase);
                    }
                    groundTile.SetTile(new Vector3Int(k,i,0), room.GetGroundTile());
                }
            }

            dir = (Direction)Random.Range(0,4);
            while (MapManager.Instance.GetRoomByCoords(nowMapCoords + GetDirection(dir)) != null) {
                dir = (Direction)Random.Range(0,4);
            }
            print(dir);

            /// 다리 제작
            Vector2Int bridgeStart = GetDoorPos(minPos, maxPos, dir);
            Vector2Int bridgeEndPos = bridgeStart;

            if (dir == Direction.Left || dir == Direction.Right) {
                bridgeEnd = bridgeStart + (new Vector2Int(bridgeSize.y, bridgeSize.x) * GetDirection(dir));
            } else {
                bridgeEnd = bridgeStart + (bridgeSize * GetDirection(dir));
            }

            if (dir == Direction.Bottom) {
                bridgeStart.y -= bridgeSize.y;
                bridgeStart.x -= bridgeSize.x / 2;

                bridgeEndPos.x += bridgeSize.x / 2;
                bridgeEndPos.y -= 1;
            } else if (dir == Direction.Top) {
                bridgeEndPos.y += bridgeSize.y;
                bridgeEndPos.x += bridgeSize.x / 2;

                bridgeStart.x -= bridgeSize.x / 2;
                bridgeStart.y += 1;
            } else if (dir == Direction.Right) {
                bridgeStart.y -= bridgeSize.x / 2;

                bridgeEndPos.y += bridgeSize.x / 2;
                bridgeEndPos.x += bridgeSize.y;

                bridgeStart.x += 1;
            } else if (dir == Direction.Left) {
                bridgeStart.x -= bridgeSize.y;
                bridgeStart.y -= bridgeSize.x / 2;

                bridgeEndPos.y += bridgeSize.x / 2;
                bridgeEndPos.x -= 1;
            }

            for (int i = bridgeStart.y; i <= bridgeEndPos.y; i++)
            {
                for (int k = bridgeStart.x; k <= bridgeEndPos.x; k++)
                {
                    bridgeTile.SetTile(new Vector3Int(k,i), bridgeBase);
                }
            }
        
        }
    }
    
    bool BoxGenerate(Direction lastDir, RoomBase lastRoom) {
        functionCall ++;
        if (createRooms.Length <= nowCreateIdx + 1) {
            return false;
        }


        // 다리 만들기
        Vector2Int bridgeEnd = Vector2Int.zero;
        Vector2Int bridgeStart = Vector2Int.zero;
        Vector2Int bridgeEndPos = Vector2Int.zero;
        BridgeBase bridge = new BridgeBase();

        if (lastRoom) {
            bridgeStart = GetDoorPos(lastRoom.MinPos, lastRoom.MaxPos, lastDir);
            bridgeEndPos = bridgeStart;

            if (lastDir == Direction.Left || lastDir == Direction.Right) {
                bridgeEnd = bridgeStart + (new Vector2Int(bridgeSize.y, bridgeSize.x) * GetDirection(lastDir));
            } else {
                bridgeEnd = bridgeStart + (bridgeSize * GetDirection(lastDir));
            }

            if (lastDir == Direction.Bottom) {
                bridgeStart.y -= bridgeSize.y;
                bridgeStart.x -= bridgeSize.x / 2;

                bridgeEndPos.x += bridgeSize.x / 2;
                bridgeEndPos.y -= 1;
            } else if (lastDir == Direction.Top) {
                bridgeEndPos.y += bridgeSize.y;
                bridgeEndPos.x += bridgeSize.x / 2;

                bridgeStart.x -= bridgeSize.x / 2;
                bridgeStart.y += 1;
            } else if (lastDir == Direction.Right) {
                bridgeStart.y -= bridgeSize.x / 2;

                bridgeEndPos.y += bridgeSize.x / 2;
                bridgeEndPos.x += bridgeSize.y;

                bridgeStart.x += 1;
            } else if (lastDir == Direction.Left) {
                bridgeStart.x -= bridgeSize.y;
                bridgeStart.y -= bridgeSize.x / 2;

                bridgeEndPos.y += bridgeSize.x / 2;
                bridgeEndPos.x -= 1;
            }

            // 테두리
            if (lastDir == Direction.Top || lastDir == Direction.Bottom) {
                bridgeStart.x -= 1;
                bridgeEndPos.x += 1;
            } else if (lastDir == Direction.Left || lastDir == Direction.Right) {
                bridgeStart.y -= 1;
                bridgeEndPos.y += 1;  
            }

            bridge.start = bridgeStart;
            bridge.end = bridgeEndPos;
        }

        // 방 만들기
        var room = Instantiate(createRooms[nowCreateIdx + 1]);
        room.SetSize();

        Vector2Int minPos = Vector2Int.zero;
        Vector2Int maxPos = Vector2Int.zero;

        if (lastDir == Direction.Top) {
            minPos = new Vector2Int(bridgeEnd.x - (room.Size.x / 2), bridgeEnd.y + 1);
            maxPos = new Vector2Int(bridgeEnd.x + (room.Size.x / 2), bridgeEnd.y + room.Size.y);
        } else if (lastDir == Direction.Bottom) {
            minPos = new Vector2Int(bridgeEnd.x - (room.Size.x / 2), bridgeEnd.y - room.Size.y);
            maxPos = new Vector2Int(bridgeEnd.x + (room.Size.x / 2), bridgeEnd.y - 1);
        } else if (lastDir == Direction.Right) {
            minPos = new Vector2Int(bridgeEnd.x + 1, bridgeEnd.y - (room.Size.y / 2));
            maxPos = new Vector2Int(bridgeEnd.x + room.Size.x, bridgeEnd.y + (room.Size.y / 2));
        } else if (lastDir == Direction.Left) {
            minPos = new Vector2Int(bridgeEnd.x - room.Size.x, bridgeEnd.y - (room.Size.y / 2));
            maxPos = new Vector2Int(bridgeEnd.x - 1, bridgeEnd.y + (room.Size.y / 2));
        }

        // 충돌 확인
        foreach (var item in MapManager.Instance.GetMapIterator())
        {
            var min = item.Value.MinPos;
            var max = item.Value.MaxPos;
            
            // 여백 줘서 방이랑 너무 가깝게 안함
            Vector2Int padding = new Vector2Int(5,5);
            Vector2Int padding_minPos = minPos - padding;
            Vector2Int padding_maxPos = maxPos + padding;

            if (padding_maxPos.x < min.x || padding_minPos.x > max.x) continue;
            if (padding_maxPos.y < min.y || padding_minPos.y > max.y) continue;

            // 박스 겹침 (만들기 중단)
            Destroy(room.gameObject);
            return false;
        }
        foreach (var item in MapManager.Instance.GetBridgeIterator())
        {
            var min = item.start;
            var max = item.end;
            if (maxPos.x < min.x || minPos.x > max.x) continue;
            if (maxPos.y < min.y || minPos.y > max.y) continue;

            // 박스 겹침 (만들기 중단)
            Destroy(room.gameObject);
            return false;
        }
        
        // bridge room 연결
        

        Vector2Int mapPos = lastRoom ? lastRoom.MapPos + GetDirection(lastDir) : Vector2Int.zero;
        MapManager.Instance.SetRoom(mapPos, room);

        room.SetRoomPos(minPos, maxPos, mapPos);

        // 바닥 / 벽
        for (int i = minPos.y; i <= maxPos.y; i++)
        {
            for (int k = minPos.x; k <= maxPos.x; k++)
            {
                if (i == minPos.y || i == maxPos.y || k == minPos.x || k == maxPos.x) {
                    wallTile.SetTile(new Vector3Int(k,i,0), wallBase);
                }
                groundTile.SetTile(new Vector3Int(k,i,0), room.GetGroundTile());
            }
        }


        // 나중에 계산 후 타일 만듬
        for (int i = 0; i < 4; i++)
        {
            Direction dir = (Direction)i;
            if (dir == ReverseD_irection(lastDir)) continue;
            RoomBase otherRoom = MapManager.Instance.GetRoomByCoords(room.MapPos + GetDirection(dir));
            if (otherRoom == null) continue;

            int halfWidthBridge = bridgeSize.x / 2;
            halfWidthBridge += 2; // 겉에 벽

            int sameCount = 0;
            int? startPos = null;
            if (dir == Direction.Bottom || dir == Direction.Top) {
                for (int k = room.MinPos.x + halfWidthBridge; k <= room.MaxPos.x - halfWidthBridge; k++)
                {
                    if (otherRoom.MinPos.x <= k && otherRoom.MaxPos.x >= k) {
                        sameCount++;
                        startPos ??= k; 
                    }
                }
            } else if (dir == Direction.Left || dir == Direction.Right) {
                for (int k = room.MinPos.y + halfWidthBridge; k <= room.MaxPos.y - halfWidthBridge; k++)
                {
                    if (otherRoom.MinPos.y <= k && otherRoom.MaxPos.y >= k) {
                        sameCount++;
                        startPos ??= k; 
                    }
                }
            }
            
            // print($"[{mapPos.x}, {mapPos.y}] ({dir.ToString()}) => {sameCount}");
            // print($"[{mapPos.x}, {mapPos.y}] center {GetDoorPos(minPos, maxPos, dir)}");
            // print($"[{mapPos.x}, {mapPos.y}] call {functionCall} {lastRoom}");
            if (sameCount >= bridgeSize.x) {
                Vector2Int modifySize = bridgeSize;
                Vector2Int endPoint = GetDoorPos(minPos, maxPos, dir);
                
                if (dir == Direction.Bottom) {
                    modifySize.y = minPos.y - otherRoom.MaxPos.y;
                    endPoint.x = startPos.Value + (sameCount / 2);
                } else if (dir == Direction.Top) {
                    modifySize.y = otherRoom.MinPos.y - maxPos.y;
                    endPoint.x = startPos.Value + (sameCount / 2);
                } else if (dir == Direction.Left) {
                    modifySize.y = minPos.x - otherRoom.MaxPos.x;
                    endPoint.y = startPos.Value + (sameCount / 2);
                } else if (dir == Direction.Right) {
                    modifySize.y = otherRoom.MinPos.x - maxPos.x;
                    endPoint.y = startPos.Value + (sameCount / 2);
                }
                modifySize.y --;

                var subBridge = CreateBridge(endPoint, dir, modifySize);
                bool isStop = false;

                foreach (var item in MapManager.Instance.GetMapIterator())
                {
                    var min = item.Value.MinPos;
                    var max = item.Value.MaxPos;
                    if (subBridge.end.x < min.x || subBridge.start.x > max.x) continue;
                    if (subBridge.end.y < min.y || subBridge.start.y > max.y) continue;

                    // 박스 겹침 (만들기 중단)
                    isStop = true;
                    break;
                }
                foreach (var item in MapManager.Instance.GetBridgeIterator())
                {
                    var min = item.start;
                    var max = item.end;
                    if (subBridge.end.x < min.x || subBridge.start.x > max.x) continue;
                    if (subBridge.end.y < min.y || subBridge.start.y > max.y) continue;

                    // 박스 겹침 (만들기 중단)
                    isStop = true;
                    break;
                }
                if (isStop) continue;
                

                for (int v = subBridge.start.y; v <= subBridge.end.y; v++)
                {
                    for (int r = subBridge.start.x; r <= subBridge.end.x; r++)
                    {
                        if (
                            ((dir == Direction.Bottom || dir == Direction.Top) && (r == subBridge.start.x || r == subBridge.end.x))
                            ||  ((dir == Direction.Left || dir == Direction.Right) && (v == subBridge.start.y || v == subBridge.end.y))
                        ) {
                            wallTile.SetTile(new Vector3Int(r,v), wallBase);
                        }
                        
                        bridgeTile.SetTile(new Vector3Int(r,v), bridgeBase);
                    }
                }

                // bridgeTile.SetTile((Vector3Int)endPoint, bridgeBase);

                subBridge.room1 = room;
                subBridge.room2 = otherRoom;

                Vector2Int doorSize;
                if (dir == Direction.Left || dir == Direction.Right) {
                    doorSize = new(subBridge.start.y, subBridge.end.y);
                } else {
                    doorSize = new(subBridge.start.x, subBridge.end.x);
                }

                room.SetDoor(dir, doorSize, subBridge);
                otherRoom.SetDoor(ReverseD_irection(dir), doorSize, subBridge);

                // 문벽 삭제
                RemoveWallDoor(room, dir, doorSize);
                // 문벽 삭제 (전 방)
                RemoveWallDoor(otherRoom, ReverseD_irection(dir), doorSize);

                MapManager.Instance.AddBridge(bridge);

                print(modifySize);
                print(endPoint);
            }
        }
        // 다리
        if (lastRoom) {
            bridge.room1 = room;
            bridge.room2 = lastRoom;
            MapManager.Instance.AddBridge(bridge);

            Vector2Int doorSize;
            int doorHeight = (bridgeSize.x / 2) + 1 /* 테투리 포함 */;
            if (lastDir == Direction.Left || lastDir == Direction.Right) {
                doorSize = new(bridgeEnd.y - doorHeight, bridgeEnd.y + doorHeight);
            } else {
                doorSize = new(bridgeEnd.x - doorHeight, bridgeEnd.x + doorHeight);
            }

            lastRoom.SetDoor(lastDir, doorSize, bridge);
            room.SetDoor(ReverseD_irection(lastDir), doorSize, bridge);

            // 문벽 삭제
            RemoveWallDoor(room, ReverseD_irection(lastDir), doorSize);
            // 문벽 삭제 (전 방)
            RemoveWallDoor(lastRoom, lastDir, doorSize);

            for (int i = bridgeStart.y; i <= bridgeEndPos.y; i++)
            {
                for (int k = bridgeStart.x; k <= bridgeEndPos.x; k++)
                {
                    if (((lastDir == Direction.Top || lastDir == Direction.Bottom) && (k == bridgeStart.x || k == bridgeEndPos.x)) || (lastDir == Direction.Left || lastDir == Direction.Right) && (i == bridgeStart.y || i == bridgeEndPos.y)) {
                        wallTile.SetTile(new Vector3Int(k,i), wallBase);
                    }

                    bridgeTile.SetTile(new Vector3Int(k,i), bridgeBase);
                }
            }
        }


        // 다 됐으면
        nowCreateIdx++;

        foreach (Direction item in GetRandomArray().Select(v => (Direction)v))
        {
            BoxGenerate(item, room);
        }
        
        room.OnComplete();
        return true;
    }

    void RemoveWallDoor(RoomBase room, Direction dir, Vector2Int size) {
        for (int i = size.x + 1; i <= size.y - 1; i++) {
            Vector2Int pos = Vector2Int.zero;
            if (dir == Direction.Top) {
                pos = new(i, room.MaxPos.y);
            } else if (dir == Direction.Right) {
                pos = new(room.MaxPos.x, i);
            } else if (dir == Direction.Left) {
                pos = new(room.MinPos.x, i);
            } else if (dir == Direction.Bottom) {
                pos = new(i, room.MinPos.y);
            }

            wallTile.SetTile(new Vector3Int(pos.x, pos.y, 0), null);
        }
    }

    public static Vector2Int GetDirection(Direction dir) {
        switch (dir)
        {
            case Direction.Top:
                return Vector2Int.up;
            case Direction.Left:
                return Vector2Int.left;
            case Direction.Right:
                return Vector2Int.right;
            case Direction.Bottom:
                return -Vector2Int.up;
        }

        return Vector2Int.zero;
    }
    public static Direction ReverseD_irection(Direction dir) {
        return dir switch
        {
            Direction.Top => Direction.Bottom,
            Direction.Left => Direction.Right,
            Direction.Right => Direction.Left,
            Direction.Bottom => Direction.Top,
            _ => 0,
        };
    }

    // min,max,dir 으로 문 위치(중점) 구하기
    Vector2Int GetDoorPos(Vector2Int min, Vector2Int max, Direction dir) {
        if (dir == Direction.Top) {
            return new Vector2Int(min.x + (max.x - min.x) / 2, max.y);
        } else if (dir == Direction.Left) {
            return new Vector2Int(min.x, min.y + (max.y - min.y) / 2);
        } else if (dir == Direction.Right) {
            return new Vector2Int(max.x, min.y + (max.y - min.y) / 2);
        } else if (dir == Direction.Bottom) {
            return new Vector2Int(min.x + (max.x - min.x) / 2, min.y);
        }

        return Vector2Int.zero;
    }

    int[] GetRandomArray() {
        int[] arr = new int[4];
        for (int i = 0; i < 4; i++)
        {
            arr[i] = i;
        }

        for (int i = 0; i < 4; i++)
        {
            int rand = Random.Range(0,4);
            int temp = arr[i];
            arr[i] = arr[rand];
            arr[rand] = temp;
        }

        return arr;
    }

    // point 기점으로 다리 범위 구함
    BridgeBase CreateBridge(Vector2Int point, Direction dir) => CreateBridge(point, dir, bridgeSize);
    BridgeBase CreateBridge(Vector2Int point, Direction dir, Vector2Int customSize) {
        // print($"CreateBridge({point},{dir},{customSize})");
        Vector2Int bridgeStart = point;
        Vector2Int bridgeEndPos = point;

        if (dir == Direction.Bottom) {
            bridgeStart.y -= customSize.y;
            bridgeStart.x -= customSize.x / 2;

            bridgeEndPos.x += customSize.x / 2;
            bridgeEndPos.y -= 1;
        } else if (dir == Direction.Top) {
            bridgeEndPos.y += customSize.y;
            bridgeEndPos.x += customSize.x / 2;

            bridgeStart.x -= customSize.x / 2;
            bridgeStart.y += 1;
        } else if (dir == Direction.Right) {
            bridgeStart.y -= customSize.x / 2;

            bridgeEndPos.y += customSize.x / 2;
            bridgeEndPos.x += customSize.y;

            bridgeStart.x += 1;
        } else if (dir == Direction.Left) {
            bridgeStart.x -= customSize.y;
            bridgeStart.y -= customSize.x / 2;

            bridgeEndPos.y += customSize.x / 2;
            bridgeEndPos.x -= 1;
        }

        // 테두리
        if (dir == Direction.Top || dir == Direction.Bottom) {
            bridgeStart.x -= 1;
            bridgeEndPos.x += 1;
        } else if (dir == Direction.Left || dir == Direction.Right) {
            bridgeStart.y -= 1;
            bridgeEndPos.y += 1;  
        }

        BridgeBase bridge = new()
        {
            start = bridgeStart,
            end = bridgeEndPos
        };
        return bridge;
    }

    public void CreateWall(Vector2Int min, Vector2Int max) {
        for (int i = min.y; i <= max.y; i++)
        {
            for (int k = min.x; k <= max.x; k++)
            {
                wallTile.SetTile(new Vector3Int(k,i,0), wallBase);
            }
        }
    }
    
    public void DeleteWall(Vector2Int min, Vector2Int max) {
        for (int i = min.y; i <= max.y; i++)
        {
            for (int k = min.x; k <= max.x; k++)
            {
                wallTile.SetTile(new Vector3Int(k,i,0), null);
            }
        }
    }

    public void CreateDoor(Vector2Int min, Vector2Int max) {
        for (int i = min.y; i <= max.y; i++)
        {
            for (int k = min.x; k <= max.x; k++)
            {
                doorTile.SetTile(new Vector3Int(k,i,0), doorBase);
            }
        }
    }
    
    public void DeleteDoor(Vector2Int min, Vector2Int max) {
        for (int i = min.y; i <= max.y; i++)
        {
            for (int k = min.x; k <= max.x; k++)
            {
                doorTile.SetTile(new Vector3Int(k,i,0), null);
            }
        }
    }

    public void CreateGround(Vector2Int pos, TileBase tile) {
        groundTile.SetTile((Vector3Int)pos, tile);
    }

    public Vector3 GetWorldCoordsByGroundCell(Vector2Int coords) {
        return groundTile.CellToWorld((Vector3Int)coords);
    }

    public Vector2Int GetGroundCellByWorldCoords(Vector3 coords) {
        return (Vector2Int)groundTile.WorldToCell(coords);
    }

    // 좌표로 타일 베이스 가져오기 (우선순위로)
    public TileBase GetTileBaseByCoords(Vector2Int pos) {
        TileBase response;
        
        response = wallTile.GetTile((Vector3Int)pos);
        if (response)
            return response;
        
        response = bridgeTile.GetTile((Vector3Int)pos);
        if (response)
            return response;
        
        response = groundTile.GetTile((Vector3Int)pos);
        if (response)
            return response;

        return null;
    }
    
    public void DeleteAll(Vector2Int coords) {
        Vector3Int pos = (Vector3Int)coords;

        wallTile.SetTile(pos, null);
        bridgeTile.SetTile(pos, null);
        groundTile.SetTile(pos, null);
        doorTile.SetTile(pos, null);
        foreach (var item in MapManager.Instance.TileManager.GetTileMapIterator())
            item.Value.SetTile(pos, null);
    }

    public MapGenerateSO GetOption() => option;
}
