using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomObstacleBig : RoomBase, IRoomObstacle
{
    List<ObstacleData> IRoomObstacle.Obstacles { get; set; }

    public override void OnComplete()
    {
        base.OnComplete();
        (this as IRoomObstacle).ObstacleInit(this);
    }


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
        base.RoomLeave();
        print("RoomLeave RoomObstacleBig");
    }

    public override Vector2Int FindPossibleRandomPos(int spacing) => (this as IRoomObstacle).FindPossibleRandomPos(this, spacing);
}
