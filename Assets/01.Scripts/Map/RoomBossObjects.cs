using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomBossObjects : RoomBoss
{
    [SerializeField] MapNearObjectSO randomSpawnSO;

    List<MapNearObjectSO.NearObject> objects;

    public override void OnComplete()
    {
        foreach (var item in randomSpawnSO.GetValue())
        {
            Vector3 pos = MapManager.Instance.GetWorldPosByCell(FindPossibleRandomPos(3));
            GameObject entity = Instantiate(item.entity, pos, Quaternion.identity);

            objects.Add(new MapNearObjectSO.NearObject() {
                spacing = item.spacing,
                entity = entity
            });
        }
    }

    public override Vector2Int FindPossibleRandomPos(int spacing)
    {
        Vector2Int pos = base.FindPossibleRandomPos(spacing);
        bool result = objects.All(v => Vector2Int.Distance(pos, MapManager.Instance.GetCellByWorldPos(v.entity.transform.position)) > (spacing / 2f) + (v.spacing / 2f));
        if (!result) {
            return FindPossibleRandomPos(spacing);
        }

        return pos;
    }
}
