using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomBase : MonoBehaviour
{
    [SerializeField] TileBase groundTile;
    public Vector2Int Size { get; protected set; }

    public Vector2Int MinPos { get; private set; }
    public Vector2Int MaxPos { get; private set; }
    public Vector2Int MapPos { get; private set; }
    private Dictionary<MapGenerator.Direction, BridgeBase> bridges = new();
    
    public virtual void SetSize() {
        Size = new Vector2Int(19,19);
    }

    public void SetRoomPos(Vector2Int min, Vector2Int max, Vector2Int mapPos) {
        MinPos = min;
        MaxPos = max;
        MapPos = mapPos;
    }

    public void SetBridge(MapGenerator.Direction dir, BridgeBase bridge) {
        bridges[dir] = bridge;
    }

    public TileBase GetGroundTile() => groundTile;

    public virtual void RoomEnter() {
        print("RoomEtner RoomBase");
    }

    public virtual void RoomLeave() {
        print("RoomLeave RoomBase");
    }
}
