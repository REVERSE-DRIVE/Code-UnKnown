using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomPurify : RoomBase
{
    [SerializeField] Tilemap mapTemplate;
    [SerializeField] TileBase tile;

    public override void OnComplete()
    {
        base.OnComplete();
        
        Tilemap purifyMap = MapManager.Instance.TileManager.GetMap(TileMapType.Purify);
        if (purifyMap == null)
            purifyMap = MapManager.Instance.TileManager.CreateMap(TileMapType.Purify, mapTemplate);

        
    }
}
