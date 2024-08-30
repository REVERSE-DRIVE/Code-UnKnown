using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

struct PurifyData {
    public Vector2Int min;
    public Vector2Int max;
}

public class RoomPurify : RoomEnemy
{
    [SerializeField] Tilemap mapTemplate;
    [SerializeField] TileBase tile;

    List<PurifyData> purifies = new();

    public override void OnComplete()
    {
        base.OnComplete();
        
        Tilemap purifyMap = MapManager.Instance.TileManager.GetMap(TileMapType.Purify);
        if (purifyMap == null)
            purifyMap = MapManager.Instance.TileManager.CreateMap(TileMapType.Purify, mapTemplate);

        const int TRY_MAX_COUNT = 5;
        int tryCount = 0; // 5번 정도면 ㄹㅇ 억까
        List<PurifyData> tempList = null;
        
        while (tryCount++ < TRY_MAX_COUNT) {
            tempList = new();
            CreatePurify(tempList);
        
            if (CheckProblem(tempList)) { // 화긴~~
                break;
            }
        }

        // 이제 진짜 마무리
        purifies = tempList;

        // 타일 깔아~~
        foreach (var item in purifies)
            for (int y = item.min.y; y <= item.max.y; y++)
                for (int x = item.min.x; x <= item.max.x; x++)
                    purifyMap.SetTile(new Vector3Int(x, y), tile);
    }

    void CreatePurify(List<PurifyData> temp_purifies) {
        int createNum = Random.Range(1, 5);
        int created = 0, failCount = 0;
        int nearSize = 3;

        while (createNum > created && failCount < 50) {
            Vector2Int size;
            Vector2Int minPos = new(), maxPos;
            bool isStop = false;

            bool width = Random.Range(0, 2) == 1;
            if (width) {
                size = new(Random.Range((int)((Size.x - 2) * 0.1f), Size.x - 2 - nearSize), Random.Range(3, 6));
            } else {
                size = new(Random.Range(3, 6), Random.Range((int)((Size.y - 2) * 0.1f), Size.y - 2 - nearSize));
            }

            // 크기 차단
            if (size.x < 5 && size.y < 5) {
                failCount++;
                continue;
            }
            
            if (size.x >= Size.x - 5 || size.y >= Size.y - 5) {
                failCount++;
                continue;
            }

            minPos.x = Random.Range( MinPos.x + 1, (MaxPos.x - size.x) + 1 );
            minPos.y = Random.Range( MinPos.y + 1, (MaxPos.y - size.y) + 1 );
            
            maxPos = minPos + size - Vector2Int.one;

            // min, max 로 룸 min max 거리 확인하고 nearDoor 보다 작으면 만들기 취소 (거리가 0일때는 그냥 함)
            Vector2Int minPosAbs = new Vector2Int(Mathf.Abs(minPos.x), Mathf.Abs(minPos.y));
            Vector2Int mapMinPosAbs = new Vector2Int(Mathf.Abs(MinPos.x), Mathf.Abs(MinPos.y));

            int maxAbsX = Mathf.Max(minPosAbs.x, mapMinPosAbs.x);
            int maxAbsY = Mathf.Max(minPosAbs.y, mapMinPosAbs.y);
            int minAbsX = Mathf.Min(minPosAbs.x, mapMinPosAbs.x);
            int minAbsY = Mathf.Min(minPosAbs.y, mapMinPosAbs.y);


            Vector2Int boxDistanceMin = new Vector2Int(maxAbsX - minAbsX, maxAbsY - minAbsY) - Vector2Int.one /* 테두리 제외 */;
            if ((boxDistanceMin.x != 0 && boxDistanceMin.x < nearSize) || (boxDistanceMin.y != 0 && boxDistanceMin.y < nearSize)) {
                failCount++;
                continue;
            }


            // MaxPos 로 비교

            Vector2Int maxPosAbs = new Vector2Int(Mathf.Abs(maxPos.x), Mathf.Abs(maxPos.y));
            Vector2Int mapMaxPosAbs = new Vector2Int(Mathf.Abs(MaxPos.x), Mathf.Abs(MaxPos.y));
            
            maxAbsX = Mathf.Max(maxPosAbs.x, mapMaxPosAbs.x);
            maxAbsY = Mathf.Max(maxPosAbs.y, mapMaxPosAbs.y);
            minAbsX = Mathf.Min(maxPosAbs.x, mapMaxPosAbs.x);
            minAbsY = Mathf.Min(maxPosAbs.y, mapMaxPosAbs.y);

            boxDistanceMin = new Vector2Int(maxAbsX - minAbsX, maxAbsY - minAbsY) - Vector2Int.one;
            if ((boxDistanceMin.x != 0 && boxDistanceMin.x < nearSize) || (boxDistanceMin.y != 0 && boxDistanceMin.y < nearSize)) {
                failCount++;
                continue;
            }


            // 문 가까운지 확인
            foreach (var item in Doors)
            {
                Vector2Int minPos2 = new();
                Vector2Int maxPos2 = new();

                if (item.Key == MapGenerator.Direction.Top) {
                    minPos2.y = MaxPos.y - nearSize;
                    minPos2.x = item.Value.size.x;
                    
                    maxPos2.y = MaxPos.y;
                    maxPos2.x = item.Value.size.y;
                } else if (item.Key == MapGenerator.Direction.Bottom) {
                    minPos2.y = MinPos.y;
                    minPos2.x = item.Value.size.x;

                    maxPos2.y = MinPos.y + nearSize;
                    maxPos2.x = item.Value.size.y;
                } else if (item.Key == MapGenerator.Direction.Left) {
                    minPos2.y = item.Value.size.x;
                    minPos2.x = MinPos.x;

                    maxPos2.y = item.Value.size.y;
                    maxPos2.x = MinPos.x + nearSize;
                } else if (item.Key == MapGenerator.Direction.Right) {
                    minPos2.y = item.Value.size.x;
                    minPos2.x = MaxPos.x - nearSize;

                    maxPos2.y = item.Value.size.y;
                    maxPos2.x = MaxPos.x;
                }

                if (maxPos2.x < minPos.x || minPos2.x > maxPos.x) continue;
                if (maxPos2.y < minPos.y || minPos2.y > maxPos.y) continue;

                isStop = true;
                break;
            }

            if (isStop) {
                failCount ++;
                continue;
            }
            // 정화구역 만들때 다른 구역 비교해서 구멍이 너무 작을 경우 스탑 처리
            foreach (var item in temp_purifies) {
                // 겹치면 상관없다.
                if (item.max.x >= minPos.x - 1 && item.min.x <= maxPos.x + 1 && item.max.y >= minPos.y - 1 && item.min.y <= maxPos.y + 1) continue;

                // 안겹치는데 ( 3 거리 안에 없음 )
                if (item.max.x < minPos.x - 3 || item.min.x > maxPos.x + 3) continue;
                if (item.max.y < minPos.y - 3 || item.min.y > maxPos.y + 3) continue;

                isStop = true;
                break;
            }

            if (isStop) {
                failCount ++;
                continue;
            }
            

            // for (int y = minPos.y; y < minPos.y + size.y; y++)
            // {
            //     for (int x = minPos.x; x < minPos.x + size.x; x++) {
            //         purifyMap.SetTile(new Vector3Int(x, y), tile);
            //     }
            // }

            temp_purifies.Add(new() {
                min = minPos,
                max = maxPos
            });

            failCount = 0;
            created ++;
        }
    }

    bool CheckProblem(List<PurifyData> temp_purifies) {
        HashSet<Vector2Int> deadPos = new();
        
        // 막힌 좌표 마킹
        foreach (var item in temp_purifies)
        {
            for (int y = item.min.y; y <= item.max.y; y++)
                for (int x = item.min.x; x <= item.max.x; x++) {
                    deadPos.Add(new() { x = x, y = y });                    
                }
        }

        // (한번씩 이동하면서 카운팅) == (현재 막히지 않은곳 카운팅) 이여야 억까가 아닌 맵임
        Vector2Int size = ((MaxPos + Vector2Int.one) - MinPos) - Vector2Int.one * 2 /* 테두리 제외 */;
        int originCount = size.x * size.y - deadPos.Count; // 원래 그거
        // int findCount = 0; // 찾은거

        HashSet<Vector2Int> groundPos = new();
        Vector2Int GetStartPos() {
            Vector2Int randPos = FindPossibleRandomPos(1);
            if (deadPos.Contains(randPos)) return GetStartPos();

            return randPos;
        }

        Vector2Int min = MinPos + Vector2Int.one;
        Vector2Int max = MaxPos;
        bool CheckGround(Vector2Int pos) {
            if (pos.x < min.x || pos.y < min.y || pos.x > max.x || pos.y > max.y) return false; // 범위가 벗어남
            if (deadPos.Contains(pos) || groundPos.Contains(pos)) return false; // 안됨
            return true;
        }

        Vector2Int nowPos = GetStartPos();
        
        while (true) {
            groundPos.Add(nowPos);

            if (CheckGround(nowPos + Vector2Int.left)) {
                nowPos += Vector2Int.left;
            } else if (CheckGround(nowPos + Vector2Int.right)) {
                nowPos += Vector2Int.right;
            } else if (CheckGround(nowPos + Vector2Int.down)) {
                nowPos += Vector2Int.down;
            } else if (CheckGround(nowPos + Vector2Int.up)) {
                nowPos += Vector2Int.up;
            } else { // 아무것도 할 수 없음
                bool isFind = false;
                foreach (var item in groundPos)
                {
                    if (CheckGround(item + Vector2Int.left) || CheckGround(item + Vector2Int.right) || CheckGround(item + Vector2Int.down) || CheckGround(item + Vector2Int.up)) {
                        nowPos = item; // 이 위치 기점으로 다시 ㄱㄱ
                        isFind = true;
                        break;
                    }
                }

                if (!isFind) // 그래도 못찾앗다
                    break;
            }
        }
        
        print($"원본 사이즈: {originCount} / 찾은 사이즈: {groundPos.Count}");
        return true;
    }

    public override Vector2Int FindPossibleRandomPos(int spacing)
    {
        Vector2Int pos = new(Random.Range(MinPos.x + 1 /* 테두리 */, MaxPos.x), Random.Range(MinPos.y + 1, MaxPos.y));
        
        Vector2Int min = pos - (Vector2Int.one * spacing);
        Vector2Int max = pos + (Vector2Int.one * spacing);

        if (MinPos.x > min.x || MinPos.y > min.y || MaxPos.x < max.x || MaxPos.y < max.y) {
            return FindPossibleRandomPos(spacing); // 다시
        }

        foreach (var item in purifies)
        {
            if (item.max.x < min.x || item.min.x > max.x) continue;
            if (item.max.y < min.y || item.min.y > max.y) continue;

            // 박스 겹침
            return FindPossibleRandomPos(spacing);
        }

        return pos;
    }
}