using System;
using System.Collections;
using System.Linq;
using ObjectManage;
using ObjectPooling;
using SaveSystem;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [field:SerializeField] public Player Player { get; private set; }
    [field:SerializeField] public Transform PlayerTrm { get; private set; }


    private void Start()
    {
        MapManager.Instance.Generate();
        GameStart();
    }

    public void GameStart()
    {
        ResetPlayer();
        

    }

    
    public void ResetPlayer()
    {
        CameraManager.Instance.ZoomDefault(15, 0.3f);
        PlayerManager.Instance.player.SetVisualActive(false);
        PlayerManager.Instance.player.MovementCompo.isStun = true;
        Vector2 startPos = MapManager.Instance.GetRoomByCoords(Vector2Int.zero).GetCenterCoords();
        PlayerManager.Instance.player.transform.position = startPos;
        EffectObject effect = PoolingManager.Instance.Pop(PoolingType.PlayerAppearVFX) as EffectObject;
        effect.Initialize(startPos);
        StartCoroutine(GameStartCoroutine());
    }

    private IEnumerator GameStartCoroutine()
    {
        yield return new WaitForSeconds(1f);
        CameraManager.Instance.ShakeHit();
        PlayerManager.Instance.player.MovementCompo.isStun = false;
        PlayerManager.Instance.player.SetVisualActive(true);
        PlayerPartManager.Instance.ChangeAllPart();
    }

    public void SaveInGameData()
    {

        PowerUpManager powerUpManager = PowerUpManager.Instance;
        
        PowerUpData[] powerUpDatas;
        powerUpDatas = powerUpManager.powerUpDictionary
            .Select(kvp => new PowerUpData(kvp.Key, kvp.Value))
            .ToArray();
        
        
        InGameData data = new InGameData()
        {
            ResourceAmount = ResourceManager.Instance.ResourceAmount,

            level = LevelManager.Instance.CurrentLevel,
            exp = LevelManager.Instance.CurrentExp,
            powerUpDatas = powerUpDatas
            
        };
        SaveManager.Instance.Save<InGameData>(data, "InGameData");
    }

    public void LoadInGameData()
    {
        
    }

    public void ExitGame()
    {
        
    }
    
}