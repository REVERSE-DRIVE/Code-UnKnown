using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "SO/Map/BossRoomSO")]
public class BossRoomSO : ScriptableObject
{
    [field: SerializeField] public string BossId { get; private set; }
    [field: SerializeField] public RandomSizeField Size { get; private set; }
    [field: SerializeField] public TileBase ground { get; private set; }
}
