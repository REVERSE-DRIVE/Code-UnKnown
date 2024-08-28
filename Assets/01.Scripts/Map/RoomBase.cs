using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public struct RoomDoorData {
    public Vector2Int size;
    public BridgeBase bridge;
}

public class RoomBase : MonoBehaviour
{
    [SerializeField] TileBase groundTile;
    [SerializeField] bool defaultDoor = false;
    [SerializeField] RandomSizeField sizeField;
    protected Transform _iconTrm;
    protected SpriteRenderer _iconRenderer;
    public Vector2Int Size { get; protected set; }

    public Vector2Int MinPos { get; private set; }
    public Vector2Int MaxPos { get; private set; }
    public Vector2Int MapPos { get; private set; }
    public Dictionary<MapGenerator.Direction, RoomDoorData> Doors { get; private set; } = new();

    protected virtual void Awake()
    {
        _iconTrm = transform.Find("MapIcon");
        _iconRenderer = _iconTrm.GetComponent<SpriteRenderer>();
    }

    public virtual void SetSize() {
        Size = sizeField.GetValue();
    }

    public void SetRoomPos(Vector2Int min, Vector2Int max, Vector2Int mapPos) {
        MinPos = min;
        MaxPos = max;
        MapPos = mapPos;
    }

    public void SetDoor(MapGenerator.Direction dir, Vector2Int size, BridgeBase bridge) {
        Doors[dir] = new() {
            size = size,
            bridge = bridge           
        };
    }
    
    public Vector2Int GetCenterPosDoor(MapGenerator.Direction dir) {
        var door = Doors[dir];

        int centerNum = (door.size.x + door.size.y) / 2;

        if (dir == MapGenerator.Direction.Top) {
            return new(centerNum, MaxPos.y + 1);
        } else if (dir == MapGenerator.Direction.Bottom) {
            return new(centerNum, MinPos.y + 1);
        } else if (dir == MapGenerator.Direction.Left) {
            return new(MinPos.x + 1, centerNum);
        } else {
            return new(MaxPos.x, centerNum);
        }
    }

    public MapGenerator.Direction ClosestDoor(Vector2Int pos) {
        MapGenerator.Direction nearDir = MapGenerator.Direction.Top;
        float nearDistance = float.MaxValue;

        foreach (var item in Doors)
        {
            Vector2Int doorPos = GetCenterPosDoor(item.Key);
            float distance = Vector2Int.Distance(pos, doorPos);

            if (nearDistance > distance) {
                nearDistance = distance;
                nearDir = item.Key;
            }
        }

        return nearDir;
    }

    public virtual Vector2Int FindPossibleRandomPos(int spacing) {
        Vector2Int pos = new(Random.Range(MinPos.x + 1 /* 테두리 */, MaxPos.x), Random.Range(MinPos.y + 1, MaxPos.y));
        
        Vector2Int min = pos - (Vector2Int.one * spacing);
        Vector2Int max = pos + (Vector2Int.one * spacing);

        if (MinPos.x > min.x || MinPos.y > min.y || MaxPos.x < max.x || MaxPos.y < max.y) {
            return FindPossibleRandomPos(spacing); // 다시
        }

        return pos;
    }

    public Vector3 GetCenterCoords() {
        Vector3 worldMin = MapManager.Instance.GetWorldPosByCell(MinPos);
        Vector3 worldMax = MapManager.Instance.GetWorldPosByCell(MaxPos + Vector2Int.one);
        
        return (worldMin + worldMax) / 2f;
    }
    

    public TileBase GetGroundTile() => groundTile;

    public virtual void RoomEnter() {
    }

    public virtual void RoomLeave() {
        HandleIconActive();
    }

    public virtual void SetDoor(bool active) {
        foreach (var item in Doors)
        {
            Vector2Int coords = (item.Key == MapGenerator.Direction.Top || item.Key == MapGenerator.Direction.Right) ? MaxPos : MinPos;
            
            Vector2Int min = coords;
            Vector2Int max = coords;

            if (item.Key == MapGenerator.Direction.Top || item.Key == MapGenerator.Direction.Bottom) {
                min.x = item.Value.size.x + 1;
                max.x = item.Value.size.y - 1;
            } else {
                min.y = item.Value.size.x + 1;
                max.y = item.Value.size.y - 1;
            }

            if (active) {
                MapManager.Instance.Generator.CreateDoor(min, max);
            } else {
                MapManager.Instance.Generator.DeleteDoor(min, max);
            }
        }
    }

    // 방 만들어짐 (bridge, min, max 등 값 안전)
    public virtual void OnComplete() {
        SetDoor(defaultDoor);
        SetMapIcon();
    }

    protected void HandleIconActive()
    {
        _iconRenderer.color = new Color(0.2f, 0.2f, 0.2f);
    }

    protected void SetMapIcon()
    {
        _iconTrm.position = GetCenterCoords();
    }

    #if UNITY_EDITOR
    private void OnDrawGizmosSelected() {
        // 모든 문 끝쪽
        foreach (var item in Doors)
        {
            Vector2Int coords;
            if (item.Key == MapGenerator.Direction.Top || item.Key == MapGenerator.Direction.Right) {
                coords = MaxPos;
            } else {
                coords = MinPos;
            }

            {
                if (item.Key == MapGenerator.Direction.Top || item.Key == MapGenerator.Direction.Bottom) {
                    coords.x = item.Value.size.x;
                } else {
                    coords.y = item.Value.size.x;
                }
                
                Gizmos.DrawWireCube(new(coords.x, coords.y, 0), Vector3.one);
            }

            {
                if (item.Key == MapGenerator.Direction.Top || item.Key == MapGenerator.Direction.Bottom) {
                    coords.x = item.Value.size.y;
                } else {
                    coords.y = item.Value.size.y;
                }
                
                Gizmos.DrawWireCube(new(coords.x, coords.y, 0), Vector3.one);
            }

        }
    }
    #endif
}
