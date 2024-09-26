using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoSingleton<MapManager>
{
    static int stageID = 0;
    static public void SetStage(int id) {
        stageID = id;
    }

    [field: SerializeField] public MapGenerator Generator { get; private set; }
    [field: SerializeField] public MapTearEffect TearEffect { get; private set; }
    public MapTileManager TileManager { get; private set; }
    [SerializeField] MapListSO mapList;
    
    Dictionary<Vector2Int, RoomBase> map = new();
    List<BridgeBase> bridges = new();
    private AudioSource _doorAudio;
    
    private void Awake() {
        TileManager = GetComponent<MapTileManager>();
        _doorAudio = GetComponent<AudioSource>();
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
    
    public void Generate() {
        MapGenerateSO stageData = mapList.GetStage(stageID);
        Generator.StartGenerate(stageData);
    }

    public void Generate(RoomBase[] templates) {
        Generator.StartGenerate(templates);
        // (비동기가 아니기 때문에 이 밑에는 맵 생성이 완료된거임)
    }

    [ContextMenu("Clear")]
    public void Clear() {
        foreach (RoomBase room in map.Values)
        {
            if (room is IRoomCleable roomClean)
                roomClean.ClearRoomObjects(true);

            Destroy(room.gameObject);
        }
        map.Clear();
        bridges.Clear();
        Generator.Clear();
        TileManager.RemoveAllMap();
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
    public bool CheckAllClear(bool success) {
        if (success) // 성공시에만 띄움
            UIManager.Instance.Open(WindowEnum.Clear);
        else
            (UIManager.Instance.GetPanel(WindowEnum.Clear) as ClearPanel).Fail();

        int existClearRoom = 0; // 방개수
        int clearRoom = 0; // 클리어된 방개수
        
        foreach (var item in map.Values)
        {
            if (item is IRoomCleable room)
            {
                existClearRoom++;

                if (room.IsRoomClear())
                    clearRoom++;
            }
        }

        //ComputerManager.Instance.SetInfect((clearRoom == 0 || existClearRoom == 0) ? 0 : (clearRoom / existClearRoom) * 100);
        //if (existClearRoom == 0 || clearRoom != existClearRoom) return false; // 클리어 할 수 있는 맵이 없음 / 다 클리어가 안됨
        
        MapGenerateSO option = Generator.GetOption();
        if (option.BossOption == null) return false; // 보스 소환할게 없넹

        // 플레이어 위치로 방을 찾음
        Vector3 playerPos = PlayerManager.Instance.player.transform.position;

        if (ComputerManager.Instance.InfectionLevel < 99)
        {
            if (clearRoom != existClearRoom)
            {
                return false;
            }
            print("구멍 생성");
            Generator.GenerateHole(playerPos);
            return false;
        }
        RoomBase level = FindRoomByCoords(GetCellByWorldPos(playerPos));
        if (level == null) return false; // 방 위치 어디임???

        Generator.BossGenerator.CreateBoss(level);
        return true;
    }

    public void PlayDoorSound()
    {
        _doorAudio.Play();
    }
}
