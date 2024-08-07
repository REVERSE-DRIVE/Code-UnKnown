using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
struct RoomObject {
    public RoomBase room;
    public byte amount;
}

[CreateAssetMenu(menuName = "SO/Map/Generate")]
public class MapGenerateSO : ScriptableObject
{
    [SerializeField] RoomObject[] rooms;
    [SerializeField] RoomBase spawnPointRoom;
    [field: SerializeField] public Vector2Int BridgeSize { get; private set; }

    public RoomBase[] GetRandomRooms() {
        int count = rooms.Sum(v => v.amount);

        RoomBase[] list = new RoomBase[count + 1];
        list[0] = spawnPointRoom;

        int k = 1;
        foreach (var item in rooms) {
            for (int i = 0; i < item.amount; i++) {
                list[k] = item.room;
                k++;
            }
        }

        for (int i = 0; i < count * 2; i++)
        {
            int idx = Random.Range(1, count + 1);
            int idx2 = Random.Range(1, count + 1);
            (list[idx], list[idx2]) = (list[idx2], list[idx]);
        }

        return list;
    }
}
