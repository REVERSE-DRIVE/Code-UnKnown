using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

struct DoorData {
    public Vector2Int size;
    public BridgeBase bridge;
}

public class RoomBase : MonoBehaviour
{
    [SerializeField] TileBase groundTile;
    public Vector2Int Size { get; protected set; }

    public Vector2Int MinPos { get; private set; }
    public Vector2Int MaxPos { get; private set; }
    public Vector2Int MapPos { get; private set; }
    private Dictionary<MapGenerator.Direction, DoorData> doors = new();
    
    public virtual void SetSize() {
        Size = new Vector2Int(19,19);
    }

    public void SetRoomPos(Vector2Int min, Vector2Int max, Vector2Int mapPos) {
        MinPos = min;
        MaxPos = max;
        MapPos = mapPos;
    }

    public void SetDoor(MapGenerator.Direction dir, Vector2Int size, BridgeBase bridge) {
        doors[dir] = new() {
            size = size,
            bridge = bridge           
        };
    }

    public TileBase GetGroundTile() => groundTile;

    public virtual void RoomEnter() {
        print("RoomEtner RoomBase");
    }

    public virtual void RoomLeave() {
        print("RoomLeave RoomBase");
    }

    public virtual void SetDoor(bool active) {
        foreach (var item in doors)
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
                MapManager.Instance.CreateWall(min, max);
            } else {
                MapManager.Instance.DeleteWall(min, max);
            }
        }
    }

    #if UNITY_EDITOR
    private void OnDrawGizmosSelected() {
        // 모든 문 끝쪽
        foreach (var item in doors)
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
