using System;
using System.Collections;
using ObjectManage;
using ObjectPooling;
using QuestManage;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [field:SerializeField] public Player Player { get; private set; }
    [field:SerializeField] public Transform PlayerTrm { get; private set; }


    protected override void Awake()
    {
        QuestObserver.Instance.ApplyAllQuest();
    }

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
    
}