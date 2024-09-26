
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum TileMapType {
    Purify,
    Debug
}

public class MapTileManager : MonoBehaviour
{
    // [SerializeField] Tilemap template;
    [SerializeField] Transform root;
    Dictionary<TileMapType, Tilemap> tilemaps = new();

    public Tilemap CreateMap(TileMapType type, Tilemap template, bool force = false) {
        if (!force && tilemaps.TryGetValue(type, out var _)) {
            Debug.LogError($"[MapTileManager] {type} 타입이 이미 생성되어있습니다.");
            return null;
        }

        var tilemap = Instantiate(template, root);
        tilemaps[type] = tilemap;
        
        return tilemap;   
    }

    public Tilemap GetMap(TileMapType type) {
        if (tilemaps.TryGetValue(type, out var tilemap)) {
            return tilemap;
        }

        return null;
    }

    public void RemoveAllMap() {
        foreach (var item in tilemaps)
            Destroy(item.Value.gameObject);

        tilemaps.Clear();
    }

    public Transform GetRoot() => root;

    public IEnumerable<KeyValuePair<TileMapType, Tilemap>> GetTileMapIterator() {
        foreach (var item in tilemaps)
            yield return item;
    }
}
