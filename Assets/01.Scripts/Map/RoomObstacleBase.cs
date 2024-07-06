using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct ObstacleData {
    public Vector2Int min;
    public Vector2Int max;
}

public class RoomObstacleBase : RoomBase
{
    List<ObstacleData> obstacles;

    public override void OnComplete()
    {
        base.OnComplete();
        
        obstacles = new();

        int createObj = Random.Range(0, (Size.x > Size.y ? Size.y : Size.x) / 3); // 장애물 갯수
        // int createObj = 10; // 장애물 갯수
        int tryCount = 0; // 시도 횟수 (너무많이 실패하면 빠져나가기 위해)
        
        while (obstacles.Count < createObj && tryCount < 50) {
            // Vector2Int min = MinPos - Vector2Int.one + new Vector2Int(Random.Range(0, Size.x - 1), Random.Range(0, Size.y - 1));
            // Vector2Int max = new Vector2Int( Random.Range(min.x, MaxPos.x), Random.Range(min.y, MaxPos.y) );

            Vector2Int size = new(Random.Range((int)(Size.x * 0.1f), Size.x), Random.Range((int)(Size.y * 0.1f), Size.y));
            if (size.x > size.y) {
                size.x = Random.Range(1, 3);
            } else if (size.x < size.y) {
                size.y = Random.Range(1, 3);
            }

            Vector2Int min = new();
            Vector2Int max = new();

            bool isStop = false;

            int rand = Random.Range(1, 4);
            if (size.x > size.y) {
                min.y = Random.Range(MinPos.y + size.y + 1, MaxPos.y - 1 - size.y * 2);
                max.y = min.y + size.y;

                if (rand == 1) {
                    min.x = MinPos.x + 1;
                    max.x = min.x + size.x;
                } else if (rand == 2) {
                    max.x = MaxPos.x - 1;
                    min.x = max.x - size.x;
                } else {
                    min.x = Random.Range(MinPos.x + 1, (MinPos.x + 1) + (Size.x - 2 - size.x));
                    max.x = min.x + size.x;
                }
            } else if (size.x < size.y) {
                min.x = Random.Range(MinPos.x + size.x + 1, MaxPos.x - 1 - size.x * 2);
                max.x = min.x + size.x;


                if (rand == 1) {
                    min.y = MinPos.y + 1;
                    max.y = min.y + size.y;
                } else if (rand == 2) {
                    max.y = MaxPos.y - 1;
                    min.y = max.y - size.y;
                } else if (rand == 3) {
                    min.y = Random.Range(MinPos.y + 1, (MinPos.y + 1) + (Size.y - 2 - size.y));
                    max.y = min.y + size.y;
                }
            } else {
                min.x = Random.Range(MinPos.x, MaxPos.x - size.x);
                min.y = Random.Range(MinPos.y, MaxPos.y - size.y);

                max = min;
                max += size;
            }

            // 크기가 너무 큼 / 크기가 너무 작음
            if (size.x + 2 + 3 >= Size.x || size.y + 2 + 3 >= Size.y || (size.x < 3 && size.y < 3)) {
                tryCount ++;
                continue;
            }

            // 겹침 확인
            foreach (var item in obstacles)
            {
                if (item.max.x < min.x - 3 || item.min.x > max.x + 3) continue;
                if (item.max.y < min.y - 3 || item.min.y > max.y + 3) continue;

                isStop = true;
                break;
            }

            // 문 가까운지 확인
            int nearSize = 5;
            foreach (var item in doors)
            {
                Vector2Int minPos = new();
                Vector2Int maxPos = new();

                if (item.Key == MapGenerator.Direction.Top) {
                    minPos.y = MaxPos.y - nearSize;
                    minPos.x = item.Value.size.x;
                    
                    maxPos.y = MaxPos.y;
                    maxPos.x = item.Value.size.y;
                } else if (item.Key == MapGenerator.Direction.Bottom) {
                    minPos.y = MinPos.y;
                    minPos.x = item.Value.size.x;

                    maxPos.y = MinPos.y + nearSize;
                    maxPos.x = item.Value.size.y;
                } else if (item.Key == MapGenerator.Direction.Left) {
                    minPos.y = item.Value.size.x;
                    minPos.x = MinPos.x;

                    maxPos.y = item.Value.size.y;
                    maxPos.x = MinPos.x + nearSize;
                } else if (item.Key == MapGenerator.Direction.Right) {
                    minPos.y = item.Value.size.x;
                    minPos.x = MaxPos.x - nearSize;

                    maxPos.y = item.Value.size.y;
                    maxPos.x = MaxPos.x;
                }

                if (maxPos.x < min.x - 1 || minPos.x > max.x + 1) continue;
                if (maxPos.y < min.y - 1 || minPos.y > max.y + 1) continue;

                isStop = true;
                break;
            }

            if (isStop) {
                tryCount ++;
                continue;
            }

            // 확정
            obstacles.Add(new ObstacleData() {
                min = min,
                max = max
            });
            tryCount = 0;

            MapManager.Instance.CreateWall(min, max);
            print($"ming / {min} ~ {max}");
        }
    }
}
