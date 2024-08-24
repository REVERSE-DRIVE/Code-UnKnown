using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomObstacleEnemy : RoomEnemy, IRoomObstacle
{
    List<ObstacleData> IRoomObstacle.Obstacles { get; set; }

    public override Vector2Int FindPossibleRandomPos(int spacing) => (this as IRoomObstacle).FindPossibleRandomPos(this, spacing);

    public override void OnComplete()
    {
        (this as IRoomObstacle).ObstacleInit(this);
        base.OnComplete();
    }
}
