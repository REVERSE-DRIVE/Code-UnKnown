using UnityEngine;

public class RoomEditor : RoomBase, IRoomCleable
{
    [SerializeField] GameObject editorObject;
    GameObject entity; // 소환된 에디터

    public override void OnComplete()
    {
        base.OnComplete();

        Vector3 worldMin = MapManager.Instance.GetWorldPosByCell(MinPos);
        Vector3 worldMax = MapManager.Instance.GetWorldPosByCell(MaxPos + Vector2Int.one);

        Vector3 worldCenter = (worldMin + worldMax) / 2f;

        entity = Instantiate(editorObject, worldCenter, Quaternion.identity);
    }

    private void OnDestroy()
    {
        // if(editorObject != null)
        //     editorObject.
    }

    public override void RoomLeave()
    {
        base.RoomLeave();

        // if (entity)
        //     Destroy(entity);
    }

    public bool IsRoomClear() => true;

    public void ClearRoomObjects(bool force)
    {
        if (force)
            Destroy(entity);
    }
}
