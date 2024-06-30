using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomEvent : MonoBehaviour
{
    Tilemap tilemap;

    public delegate void RoomInOut(bool join, GameObject entity, Vector3Int pos);
    public event RoomInOut OnRoomInOut;
    
    private void Awake() {
        tilemap = GetComponent<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Vector3Int pos = tilemap.WorldToCell(other.transform.position);
        OnRoomInOut?.Invoke(true, other.gameObject, pos);
    }

    private void OnTriggerExit2D(Collider2D other) {
        Vector3Int pos = tilemap.WorldToCell(other.transform.position);
        OnRoomInOut?.Invoke(false, other.gameObject, pos);
    }
}
