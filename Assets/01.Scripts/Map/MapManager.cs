using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoSingleton<MapManager>
{
    [SerializeField] MapGenerator generator;

    Dictionary<Vector2Int, RoomBase> map = new();
    List<BridgeBase> bridges = new();

    public RoomBase GetRoomByCoords(Vector2Int coords) {
        bool result = map.TryGetValue(coords, out var room);
        return result ? room : null;
    }

    public void SetRoom(Vector2Int coords, RoomBase room) {
        map[coords] = room;
    }

    public void AddBridge(BridgeBase bridge) {
        bridges.Add(bridge);
    }

    public void Clear() {
        map.Clear();
        bridges.Clear();
    }

    public IEnumerable<KeyValuePair<Vector2Int, RoomBase>> GetMapIterator() {
        foreach (var item in map)
            yield return item;
    }

    public IEnumerable<BridgeBase> GetBridgeIterator() {
        foreach (var item in bridges)
            yield return item;
    }

    public RoomBase FindRoomByCoords(Vector2 pos) {
        foreach (var item in map)
        {
            if (pos.x >= item.Value.MinPos.x && pos.x <= item.Value.MaxPos.x && pos.y >= item.Value.MinPos.y && pos.y <= item.Value.MaxPos.y) {
                return item.Value;
            }
        }

        return null;
    }

    public void CreateWall(Vector2Int min, Vector2Int max) {
        generator.CreateWall(min, max);
    }
    public void DeleteWall(Vector2Int min, Vector2Int max) {
        generator.DeleteWall(min, max);
    }
}
