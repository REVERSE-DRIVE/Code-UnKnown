using System;
using PlayerPartsManage;
using UnityEngine;

public class PlayerPartManager : MonoSingleton<PlayerPartManager>
{
    public PlayerBodyPartDataSO BodyPart { get; private set; }
    public PlayerLegPartDataSO LegPart { get; private set; }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void SetBodyPart(PlayerBodyPartDataSO bodyPart)
    {
        BodyPart = bodyPart;
    }
    
    public void SetLegPart(PlayerLegPartDataSO legPart)
    {
        LegPart = legPart;
    }

    public void ChangePart()
    {
        var partController = PlayerManager.Instance.player.PlayerPartController;
        if (partController == null)
        {
            Debug.LogWarning("PlayerPartController is null");
            return;
        }
        partController.ChangePart(PartType.Body, BodyPart);
        partController.ChangePart(PartType.Leg, LegPart);
    }
}