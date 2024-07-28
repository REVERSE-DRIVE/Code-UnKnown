using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLaser : RoomBase
{
    [SerializeField] VirusSuppressorObject suppressorPrefab;
    [SerializeField] LaserObject laserPrefab;

    [SerializeField] Vector2Int minMax = new(4, 8);

    public override void OnComplete()
    {
        base.OnComplete();

        // 바이러스 억제기
        var suppressor = Instantiate(suppressorPrefab, GetCenterCoords(), Quaternion.identity);

        // 레이정
        for (int i = 0; i < Random.Range(minMax.x, minMax.y); i++)
        {
            Vector3 pos = MapManager.Instance.GetWorldPosByCell(GetRandomCoords());
            LaserObject laser = Instantiate(laserPrefab, pos, Quaternion.identity);

            // 타입 정하기
            int laserType;
            if (i < 2)
                laserType = i; // 빨, 초 무조건 있어야함
            else
                laserType = Random.Range(0, 2);

            laser.Init((LaserObject.Type)laserType, suppressor.transform);
        }


    }

    HashSet<Vector2Int> alreadyPos = new();
    Vector2Int GetRandomCoords() {
        Vector2Int pos = FindPossibleRandomPos(3);
        
        if (alreadyPos.Contains(pos))
            return GetRandomCoords();

        alreadyPos.Add(pos);
        return pos;
    }
}
