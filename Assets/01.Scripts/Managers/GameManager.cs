using System;
using ObjectManage;
using ObjectPooling;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [field:SerializeField] public Player Player { get; private set; }
    [field:SerializeField] public Transform PlayerTrm { get; private set; }


    private void Start()
    {
        GameStart();
    }

    public void GameStart()
    {
        Vector2 startPos = MapManager.Instance.GetRoomByCoords(Vector2Int.zero).GetCenterCoords();
        PlayerManager.Instance.player.transform.position = startPos;
        EffectObject effect = PoolingManager.Instance.Pop(PoolingType.PlayerAppearVFX) as EffectObject;
        effect.Initialize(startPos);
    }
    
}