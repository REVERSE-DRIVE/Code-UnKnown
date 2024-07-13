using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomObstacleBig : RoomObstacleBase
{
    public override void SetSize()
    {
        Size = new Vector2Int(Random.Range(10, 60), Random.Range(10, 60));
    }

    public override void RoomEnter()
    {
        print("RoomEnter RoomObstacleBig");
    }

    public override void RoomLeave()
    {
        print("RoomLeave RoomObstacleBig");
    }
}
