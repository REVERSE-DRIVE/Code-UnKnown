using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBig : RoomBase
{
    public override void SetSize()
    {
        Size = new Vector2Int(Random.Range(10, 60), Random.Range(10, 60));
    }

    public override void RoomEnter()
    {
        print("RoomEnter RoomBig");
    }

    public override void RoomLeave()
    {
        base.RoomLeave();
        print("RoomLeave RoomBig");
    }
}
