using System.Collections;
using System.Collections.Generic;
using ObjectManage;
using ObjectPooling;
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
        //Generate();
        
        // 만든 후 준비 방으로 플레이어 이동
        // RoomBase room = GetRoomByCoords(Vector2Int.zero);
        // Vector3 centerPos = room.GetCenterCoords();
        //
        // PlayerManager.Instance.player.transform.position = centerPos;
        // Generator.BossGenerator.CreateBoss(GetRoomByCoords(Vector2Int.zero));

        // 방 찢기기 테스트
        // TearEffect.TearMap(GetRoomByCoords(Vector2Int.zero));

        // var enumerator = map.GetEnumerator();
        // enumerator.MoveNext();
        // enumerator.MoveNext();
        // enumerator.MoveNext();
        // enumerator.MoveNext();

        // TearEffect.TearMap(enumerator.Current.Value);
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

    // 보스가 나올 차례?
    public bool CheckAllClear() {
        UIManager.Instance.Open(WindowEnum.Clear);

        int existClearRoom = 0;
        int clearRoom = 0;
        
        foreach (var item in map.Values)
        {
            if (item is IRoomCleable room)
            {
                existClearRoom++;

                if (room.IsRoomClear())
                    clearRoom++;
            }
        }

        ComputerManager.Instance.SetInfect((clearRoom == 0 || existClearRoom == 0) ? 0 : (clearRoom / existClearRoom) * 100);
        if (existClearRoom == 0 || clearRoom != existClearRoom) return false; // 클리어 할 수 있는 맵이 없음 / 다 클리어가 안됨
        
        MapGenerateSO option = Generator.GetOption();
        if (option.BossOption == null) return false; // 보스 소환할게 없넹

        // 플레이어 위치로 방을 찾음
        Vector3 playerPos = PlayerManager.Instance.player.transform.position;
        RoomBase level = FindRoomByCoords(GetCellByWorldPos(playerPos));
        if (level == null) return false; // 방 위치 어디임???

        Generator.BossGenerator.CreateBoss(level);
        return true;
    }
}
