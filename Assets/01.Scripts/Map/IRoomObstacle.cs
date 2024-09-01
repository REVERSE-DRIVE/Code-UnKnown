using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ObstacleData {
    public Vector2Int min;
    public Vector2Int max;
}


public interface IRoomObstacle
{
    List<ObstacleData> Obstacles { get; protected set; }

    void ObstacleInit(RoomBase room) {
        Obstacles = new();

        int createObj = Random.Range(0, (room.Size.x > room.Size.y ? room.Size.y : room.Size.x) / 3); // 장애물 갯수
        // int createObj = 10; // 장애물 갯수
        int tryCount = 0; // 시도 횟수 (너무많이 실패하면 빠져나가기 위해)
        
        while (Obstacles.Count < createObj && tryCount < 50) {
            // Vector2Int min = room.MinPos - Vector2Int.one + new Vector2Int(Random.Range(0, room.Size.x - 1), Random.Range(0, room.Size.y - 1));
            // Vector2Int max = new Vector2Int( Random.Range(min.x, room.MaxPos.x), Random.Range(min.y, room.MaxPos.y) );

            Vector2Int size = new(Random.Range((int)(room.Size.x * 0.1f), room.Size.x), Random.Range((int)(room.Size.y * 0.1f), room.Size.y));
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
                int rangeMin2 = room.MinPos.y + size.y + 1;
                int rangeMax2 = room.MaxPos.y - 1 - size.y * 2;
                min.y = Random.Range(rangeMin2, rangeMax2);

                // if (min.y == rangeMin2 + 1) {
                //     if (min.y + 1 < rangeMax2) // 범위 벗어남
                //         min.y++;
                //     else
                //         min.y--;

                // } else if (min.y == rangeMax2 - 2 /* max가 포함되어있지 않아서 2 뺌 */) {
                //     if (min.y - 1 >= rangeMin2) // 범위 벗어남
                //         min.y--;
                //     else
                //         min.y++;
                // }

                max.y = min.y + size.y;

                if (rand == 1) {
                    min.x = room.MinPos.x + 1;
                    max.x = min.x + size.x;
                } else if (rand == 2) {
                    max.x = room.MaxPos.x - 1;
                    min.x = max.x - size.x;
                } else {
                    int rangeMin = room.MinPos.x + 1;
                    int rangeMax = (room.MinPos.x + 1) + (room.Size.x - 2 - size.x);
                    min.x = Random.Range(rangeMin, rangeMax);

                    if (min.x == rangeMin + 1) {
                        min.x --; // 다시 뒤로 가
                    } else if (min.x == rangeMax - 2) {
                        min.x ++; // 다시 앞으로 가
                    }

                    max.x = min.x + size.x;
                }
            } else if (size.x < size.y) {
                int rangeMin2 = room.MinPos.x + size.x + 1;
                int rangeMax2 = room.MaxPos.x - 1 - size.x * 2;
                min.x = Random.Range(rangeMin2, rangeMax2);
                
                // if (min.x == rangeMin2 + 1) {
                //     if (min.x + 1 < rangeMax2) // 범위 벗어남
                //         min.x++;
                //     else
                //         min.x--;

                // } else if (min.x == rangeMax2 - 2 /* max가 포함되어있지 않아서 2 뺌 */) {
                //     if (min.x - 1 >= rangeMin2) // 범위 벗어남
                //         min.x--;
                //     else
                //         min.x++;
                // }

                max.x = min.x + size.x;

                if (rand == 1) {
                    min.y = room.MinPos.y + 1;
                    max.y = min.y + size.y;
                } else if (rand == 2) {
                    max.y = room.MaxPos.y - 1;
                    min.y = max.y - size.y;
                } else if (rand == 3) {
                    int rangeMin = room.MinPos.y + 1;
                    int rangeMax = (room.MinPos.y + 1) + (room.Size.y - 2 - size.y);
                    min.y = Random.Range(rangeMin, rangeMax);

                    if (min.y == rangeMin + 1) {
                        min.y --; // 다시 뒤로 가
                    } else if (min.y == rangeMax - 2) {
                        min.y ++; // 다시 앞으로 가
                    }
                    
                    max.y = min.y + size.y;
                }
            } else {
                min.x = Random.Range(room.MinPos.x, room.MaxPos.x - size.x);
                min.y = Random.Range(room.MinPos.y, room.MaxPos.y - size.y);

                max = min;
                max += size;
            }

            // 크기가 너무 큼 / 크기가 너무 작음
            if (size.x + 2 + 3 >= room.Size.x || size.y + 2 + 3 >= room.Size.y || (size.x < 3 && size.y < 3)) {
                tryCount ++;
                continue;
            }

            // 겹침 확인
            foreach (var item in Obstacles)
            {
                if (item.max.x < min.x - 3 || item.min.x > max.x + 3) continue;
                if (item.max.y < min.y - 3 || item.min.y > max.y + 3) continue;

                isStop = true;
                break;
            }

            // 문 가까운지 확인
            int nearSize = 5;
            foreach (var item in room.Doors)
            {
                Vector2Int minPos = new();
                Vector2Int maxPos = new();

                if (item.Key == MapGenerator.Direction.Top) {
                    minPos.y = room.MaxPos.y - nearSize;
                    minPos.x = item.Value.size.x;
                    
                    maxPos.y = room.MaxPos.y;
                    maxPos.x = item.Value.size.y;
                } else if (item.Key == MapGenerator.Direction.Bottom) {
                    minPos.y = room.MinPos.y;
                    minPos.x = item.Value.size.x;

                    maxPos.y = room.MinPos.y + nearSize;
                    maxPos.x = item.Value.size.y;
                } else if (item.Key == MapGenerator.Direction.Left) {
                    minPos.y = item.Value.size.x;
                    minPos.x = room.MinPos.x;

                    maxPos.y = item.Value.size.y;
                    maxPos.x = room.MinPos.x + nearSize;
                } else if (item.Key == MapGenerator.Direction.Right) {
                    minPos.y = item.Value.size.x;
                    minPos.x = room.MaxPos.x - nearSize;

                    maxPos.y = item.Value.size.y;
                    maxPos.x = room.MaxPos.x;
                }

                if (maxPos.x < min.x - 1 || minPos.x > max.x + 1) continue;
                if (maxPos.y < min.y - 1 || minPos.y > max.y + 1) continue;

                isStop = true;
                break;
            }

            // 너무 붙어있지 않은지 확인
            Vector2Int diffMin = min - room.MinPos;
            Vector2Int diffMax = room.MaxPos - max;
            if (Mathf.Abs(diffMax.x) == 2 || Mathf.Abs(diffMax.y) == 2 || Mathf.Abs(diffMin.x) == 2 || Mathf.Abs(diffMin.y) == 2) {
                isStop = true;
            }

            if (isStop) {
                tryCount ++;
                continue;
            }

            // 확정
            Obstacles.Add(new ObstacleData() {
                min = min,
                max = max
            });
            tryCount = 0;

            MapManager.Instance.CreateWall(min, max);
            // Debug.Log($"ming / {min} ~ {max}");
    }
}

    public Vector2Int FindPossibleRandomPos(RoomBase room, int spacing) {
        Vector2Int pos = new(Random.Range(room.MinPos.x + 1 /* 테두리 */, room.MaxPos.x), Random.Range(room.MinPos.y + 1, room.MaxPos.y));
        
        Vector2Int min = pos - (Vector2Int.one * spacing);
        Vector2Int max = pos + (Vector2Int.one * spacing);

        if (room.MinPos.x > min.x || room.MinPos.y > min.y || room.MaxPos.x < max.x || room.MaxPos.y < max.y) {
            return FindPossibleRandomPos(room, spacing); // 다시
        }

        foreach (var item in Obstacles)
        {
            if (item.max.x < min.x || item.min.x > max.x) continue;
            if (item.max.y < min.y || item.min.y > max.y) continue;

            // 박스 겹침
            return FindPossibleRandomPos(room, spacing);
        }

        return pos;
    }
}