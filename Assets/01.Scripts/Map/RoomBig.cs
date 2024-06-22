using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBig : RoomBase
{
    public override void SetSize()
    {
        Size = new Vector2Int(Random.Range(10, 100), Random.Range(10, 100));
    }
}
