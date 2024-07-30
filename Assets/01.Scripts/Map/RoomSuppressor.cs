using System.Collections;
using System.Collections.Generic;
using ObjectPooling;
using UnityEngine;

public class RoomSuppressor : RoomBase, IRoomObstacle
{
    [SerializeField] ZipSuppressorObject zipObject;

    [Header("time setting")]
    [SerializeField] int openDelay = 120;
    // [SerializeField, Tooltip("페이즈 횟수")] int phaseCount = 3;
    [SerializeField, Tooltip("페이즈 간격")] int spawnInterval = 30;
    [SerializeField, Space] PhaseSpawnEntitySO[] phaseEnemys;

    List<EnemyBase> enemys; // 스폰된 에너미들
    List<UnityEngine.Events.UnityAction> enemyEvents; // 스폰된 에너미들
    int currentPhase = 0; // 페이즈 횟수
    int timer = 0;
    ZipSuppressorObject currentZip;

    List<ObstacleData> IRoomObstacle.Obstacles { get; set; }

    public override void OnComplete()
    {
        // (this as IRoomObstacle).ObstacleInit(this);
        currentZip = Instantiate(zipObject, GetCenterCoords(), Quaternion.identity);
        currentZip.Init(this, openDelay);

        currentZip.OnInteractEvent += StartZipOpen;
    }
    
    void StartZipOpen() {
        SetDoor(true);
        enemys = new();
        enemyEvents = new();
        timer = openDelay;

        StartCoroutine(PhaseSpawnEnemy());
        StartCoroutine(TimeHandler());
    }

    void EnemySpawn(PhaseSpawnEntitySO data) {
        RandomPercentUtil<PoolingType> randUtil = new(data.enemys);
        
        for (int i = 0; i < data.amount; i++)
        {
            EnemyBase enemy = PoolingManager.Instance.Pop(randUtil.GetValue()) as EnemyBase;
            Vector2Int pos = FindPossibleRandomPos(3);
            enemy.transform.position = MapManager.Instance.GetWorldPosByCell(pos);
            
            int idx = enemyEvents.Count;
            enemyEvents.Add(() => EnemyDied(idx, enemy));
            enemys.Add(enemy);

            enemy.HealthCompo.OnDieEvent.AddListener(enemyEvents[idx]);
        }
    }

    IEnumerator PhaseSpawnEnemy() {
        for (int i = 0; i < phaseEnemys.Length; i++)
        {
            EnemySpawn(phaseEnemys[i]);
            currentPhase = i + 1;
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    IEnumerator TimeHandler() {
        while (--timer > 0) {
            yield return new WaitForSeconds(1);
            if (timer < 0) yield break;
        }

        // 여기까지 왔다면 모든 적을 처치하지 못함
        Destroy(currentZip.gameObject);
    }

    void EnemyDied(int idx, EnemyBase enemy) {
        enemy.HealthCompo.OnDieEvent.RemoveListener(enemyEvents[idx]);
        enemys.Remove(enemy);

        if (currentPhase < phaseEnemys.Length || enemys.Count > 0) return; // 아직 남아있음
        
        if (timer > 0) { // 모든 적 처치 완료
            timer = -1;
            currentZip.Open();
        }

        SetDoor(false);
    }
}
