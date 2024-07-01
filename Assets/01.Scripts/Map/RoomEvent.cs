using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomEvent : MonoBehaviour
{
    Tilemap tilemap;
    new Collider2D collider;
    Vector2Int[] crossDir = new Vector2Int[] {
        new(0,0),
        new(0,1),
        new(0,-1),
        new(1,0),
        new(-1,0)
    };
    RoomBase lastJoinRoom = null;

    private void Awake() {
        tilemap = GetComponent<Tilemap>();
        collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // 충돌 지점
        Vector2 point = collider.ClosestPoint(other.transform.position);
        Vector2Int tilePos = (Vector2Int)tilemap.WorldToCell(point);

        RoomBase room = null;
        foreach (var item in crossDir)
        {
            room = MapManager.Instance.FindRoomByCoords(tilePos + item);
            
            if (room != null) break;
        }
        if (room == null) return;
        
        if (lastJoinRoom != null)
            lastJoinRoom.RoomLeave();

        room.RoomEnter();
        lastJoinRoom = room;
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (lastJoinRoom == null) return;

        lastJoinRoom.RoomLeave();
        lastJoinRoom = null;
    }
}
