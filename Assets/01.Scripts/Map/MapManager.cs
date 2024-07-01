using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoSingleton<MapManager>
{
    Dictionary<Vector2Int, RoomBase> map = new();
    List<BridgeBase> bridges = new();

    public RoomBase GetRoomByCoords(Vector2Int coords) {
        bool result = map.TryGetValue(coords, out var room);
        return result ? room : null;
    }

    public void SetRoom(Vector2Int coords, RoomBase room) {
        map[coords] = room;
    }

    public void AddBrdige(BridgeBase bridge) {
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
}
