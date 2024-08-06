﻿using System;
using System.Collections.Generic;
using PlayerPartsManage;
using UnityEngine;

public class PlayerPartManager : MonoSingleton<PlayerPartManager>
{
    private List<PlayerPartDataSO> _playerPartDataList;
    [field:SerializeField] public PlayerBodyPartDataSO BodyPart { get; private set; }
    [field:SerializeField] public PlayerLegPartDataSO LegPart { get; private set; }

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

    public void ChangeAllPart()
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
    
    public void ChangePart(PartType partType, PlayerPartDataSO partData)
    {
        var partController = PlayerManager.Instance.player.PlayerPartController;
        if (partController == null)
        {
            Debug.LogWarning("PlayerPartController is null");
            return;
        }
        partController.ChangePart(partType, partData);
    }
    
    public void AddPartData(PlayerPartDataSO partData)
    {
        if (_playerPartDataList == null)
            _playerPartDataList = new List<PlayerPartDataSO>();
        _playerPartDataList.Add(partData);
    }
}