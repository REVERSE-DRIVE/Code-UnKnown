using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoSingleton<BossManager>
{
    public BossInfoListSO table;
    [SerializeField] private BossCutScenePanel _cutScenePanel;
    [SerializeField] private BossHealthBar _healthBar;
    public BossInfoSO currentBossInfo;
    public Boss currentBoss;
    
    public void GenerateBoss(int id, Vector2 position)
    {
        currentBossInfo = table.Find(id);
        if (currentBossInfo == null)
        {
            Debug.LogWarning($"Wrong Boss Id ({id})");
        }
        
        currentBoss = Instantiate(currentBossInfo.bossPrefab, position, Quaternion.identity);
        _cutScenePanel.Initialize(currentBossInfo);
        _cutScenePanel.Open();
        _healthBar.Initialize(currentBossInfo, currentBoss);
    }

}
