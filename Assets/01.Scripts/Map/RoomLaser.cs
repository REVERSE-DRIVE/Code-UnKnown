using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomLaser : RoomBase, IRoomCleable
{
    [SerializeField] VirusSuppressorObject suppressorPrefab;
    [SerializeField] LaserObject laserPrefab;
    [SerializeField] JunkFileObject junkFile;

    [SerializeField] Vector2Int laserMinMax = new(4, 8);
    [SerializeField] Vector2Int junkMinMax = new(3, 6);

    [SerializeField] int clearTime;

    List<LaserObject> lasers;
    List<JunkFileObject> junks;
    VirusSuppressorObject suppressor;
    HashSet<Vector2Int> alreadyPos;
    bool isClear = false;

    private void OnDestroy()
    {
        foreach (LaserObject laser in lasers)
        {
            if (laser == null) continue;
            Destroy(laser.gameObject);
        }
        lasers.Clear();
    }

    public override void OnComplete()
    {
        base.OnComplete();

        lasers = new();
        alreadyPos = new();
        junks = new();

        // 바이러스 억제기
        Vector3 centerPos = GetCenterCoords();
        suppressor = Instantiate(suppressorPrefab, centerPos, Quaternion.identity);

        int spacing = 3;
        Vector2Int centerCell = MapManager.Instance.GetCellByWorldPos(centerPos);
        for (int y = centerCell.y - spacing; y <= centerCell.y + spacing; y++)
            for (int x = centerCell.x - spacing; x <= centerCell.x + spacing; x++)
                alreadyPos.Add(new(x, y));


        // 레이정
        int greenLaserAmount = 0;
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

            if (i == 1 || (laserType == (int)LaserObject.Type.Green && Random.Range(0, 2) == Random.Range(0, 2)))
                laser.SetMove(true);

            if (laserType == (int)LaserObject.Type.Red)
                laser.OnRemove += OnRedLaserDestroy;
            else
                greenLaserAmount ++;
        }

        // 정크 파일
        for (int i = 0; i < greenLaserAmount + 2; i++)
        {
            var entity = Instantiate(junkFile, GetRandomCoords(), Quaternion.identity);
            junks.Add(entity);
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
        suppressor.OnClear += AllClear;
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
        TimerManager.Instance.ShowTimer(clearTime);
        TimerManager.Instance.OnFinish += OnTimeEnd;

        NotifyPanel.Instance.Show("초록색 레이저는 억제기를 방해합니다.", 8);
    }

    void OnTimeEnd() {
        OnClear();

        // 의뢰 완성도 감소
        // ...
    }

    void OnRedLaserDestroy() { // 레이저 뿌셔서 끝남
        if (isClear) return;

        TimerManager.Instance.CancelTimer();
        OnClear();

        // 의뢰 완성도 감소
        // ...
    }

    void AllClear() {
        if (isClear) return;
        
        TimerManager.Instance.CancelTimer();
        OnClear(true);
    }

    void OnClear(bool success = false) {
        isClear = true;
        SetDoor(false);

        MapManager.Instance.CheckAllClear(success);
    }

    public bool IsRoomClear() => isClear;

    public void ClearRoomObjects(bool force)
    {
        if (force) {
            lasers.ForEach(v => {
                if (v.gameObject)
                    Destroy(v.gameObject);
            });
            junks.ForEach(v => {
                if (v.gameObject)
                    Destroy(v.gameObject);
            });
            return;
        }

        lasers.ForEach(v => {
            if (v != null && v.enabled) {
                v.enabled = false;
                MapManager.Instance.TearEffect.RegisterTearObject(v.gameObject);
            }
        });
        lasers.Clear();

        junks.ForEach(v => Destroy(v.gameObject));
        junks.Clear();

        MapManager.Instance.TearEffect.RegisterTearObject(suppressor.gameObject);
    }
}
