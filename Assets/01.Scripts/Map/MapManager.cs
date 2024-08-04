using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoSingleton<MapManager>
{
    [field: SerializeField] public MapGenerator Generator { get; private set; }
    [field: SerializeField] public MapTearEffect TearEffect { get; private set; }
    public MapTileManager TileManager { get; private set; }

    Dictionary<Vector2Int, RoomBase> map = new();
    List<BridgeBase> bridges = new();

    private void Awake() {
        TileManager = GetComponent<MapTileManager>();
    }

    ///////////////////////////////// TEST
    private void Start() {
        Generate();
        
        // 만든 후 준비 방으로 플레이어 이동
        RoomBase room = GetRoomByCoords(Vector2Int.zero);
        Vector3 centerPos = room.GetCenterCoords();

        PlayerManager.Instance.player.transform.position = centerPos;
    }
    ///////////////////////////////// TEST END

    public RoomBase GetRoomByCoords(Vector2Int coords) {
        bool result = map.TryGetValue(coords, out var room);
        return result ? room : null;
    }
    
    public Vector3 GetWorldPosByCell(Vector2Int cell) => Generator.GetWorldCoordsByGroundCell(cell);
    public Vector2Int GetCellByWorldPos(Vector3 cell) => Generator.GetGroundCellByWorldCoords(cell);

    public void SetRoom(Vector2Int coords, RoomBase room) {
        map[coords] = room;
    }

    public void AddBridge(BridgeBase bridge) {
        bridges.Add(bridge);
    }
    
    public void Generate() => Generator.StartGenerate();

    public void Generate(RoomBase[] templates) {
        Generator.StartGenerate(templates);
        // (비동기가 아니기 때문에 이 밑에는 맵 생성이 완료된거임)
    }

    public void Clear() {
        map.Clear();
        bridges.Clear();
        Generator.Clear();
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
        Generator.CreateWall(min, max);
    }
    public void DeleteWall(Vector2Int min, Vector2Int max) {
        Generator.DeleteWall(min, max);
    }
}
