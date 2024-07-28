using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLaser : RoomBase
{
    [SerializeField] VirusSuppressorObject suppressorPrefab;
    [SerializeField] LaserObject laserPrefab;
    [SerializeField] JunkFileObject junkFile;

    [SerializeField] Vector2Int laserMinMax = new(4, 8);
    [SerializeField] Vector2Int junkMinMax = new(3, 6);

    [SerializeField] int clearTime;

    List<LaserObject> lasers;
    HashSet<Vector2Int> alreadyPos;
    bool isClear = false;
    int timer;

    public override void OnComplete()
    {
        base.OnComplete();

        lasers = new();
        alreadyPos = new();

        // 바이러스 억제기
        Vector3 centerPos = GetCenterCoords();
        var suppressor = Instantiate(suppressorPrefab, centerPos, Quaternion.identity);

        int spacing = 3;
        Vector2Int centerCell = MapManager.Instance.GetCellByWorldPos(centerPos);
        for (int y = centerCell.y - spacing; y <= centerCell.y + spacing; y++)
            for (int x = centerCell.x - spacing; x <= centerCell.x + spacing; x++)
                alreadyPos.Add(new(x, y));


        // 레이정
        for (int i = 0; i < Random.Range(laserMinMax.x, laserMinMax.y + 1); i++)
        {
            LaserObject laser = Instantiate(laserPrefab, GetRandomCoords(), Quaternion.identity);

            // 타입 정하기
            int laserType;
            if (i < 2)
                laserType = i; // 빨, 초 무조건 있어야함
            else
                laserType = Random.Range(0, 2);

            laser.Init((LaserObject.Type)laserType, suppressor.transform);
            lasers.Add(laser);

            if (laserType == (int)LaserObject.Type.Red)
                laser.OnRemove += OnRedLaserDestroy;
        }

        // 정크 파일
        for (int i = 0; i < Random.Range(junkMinMax.x, junkMinMax.y + 1); i++)
        {
            Instantiate(junkFile, GetRandomCoords(), Quaternion.identity);
        }

        // 위치 재정의
        foreach (var laser in lasers)
        {
            int fail = 0;

            laser.LookAt();
            while ((laser.Beam().transform != suppressor.transform || HasBadPos(laser)) && fail < 50) { // 레이저가 억제기에 안맞으면
                fail ++;
                
                laser.transform.position = GetRandomCoords();
                laser.LookAt();
            }

            laser.ForceHit(suppressor);
        }
    
        // 확정
        suppressor.Init(lasers);
        suppressor.OnClear += OnClear;
    }

    Vector3 GetRandomCoords() {
        Vector2Int pos = FindPossibleRandomPos(3);
        
        if (alreadyPos.Contains(pos))
            return GetRandomCoords();

        return MapManager.Instance.GetWorldPosByCell(pos);
    }

    // 안좋은 자리?
    bool HasBadPos(LaserObject my) {
        foreach (var item in lasers)
        {
            if (my == item) continue;
            if (Vector2.Distance(item.transform.position, my.transform.position) < 3f) return true;
        }

        return false;
    }

    public override void RoomEnter()
    {
        base.RoomEnter();
        
        if (isClear) return;

        Transform player = PlayerManager.Instance.player.transform;
        Vector2Int playerPos = MapManager.Instance.GetCellByWorldPos(player.position);

        MapGenerator.Direction doorDir = ClosestDoor(playerPos);
        Vector2Int doorPos = GetCenterPosDoor(doorDir) +  -MapGenerator.GetDirection(doorDir) * 2;

        player.position = MapManager.Instance.GetWorldPosByCell(doorPos);

        SetDoor(true);
        StartCoroutine(TimeHandler());
    }

    IEnumerator TimeHandler() {
        while (--timer > 0) {
            yield return new WaitForSeconds(1);
            if (timer < 0) yield break;
        }

        // 의뢰 완성도 감소
        // ...
    }

    void OnRedLaserDestroy() {
        if (isClear) return;
        
        // 의뢰 완성도 감소
        // ...
    }

    void OnClear() {
        timer = -1;
        isClear = true;
        SetDoor(false);
    }
}
