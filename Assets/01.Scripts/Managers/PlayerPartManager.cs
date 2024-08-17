using System;
using System.Collections.Generic;
using PlayerPartsManage;
using SaveSystem;
using UnityEngine;

public class PlayerPartManager : MonoSingleton<PlayerPartManager>
{
    [SerializeField] private PlayerPartTableSO _partTable;
    [field:SerializeField] public List<PlayerPartDataSO> PlayerPartDataList { get; set; }
    [field:SerializeField] public PlayerBodyPartDataSO BodyPart { get; private set; }
    [field:SerializeField] public PlayerLegPartDataSO LegPart { get; private set; }
    
    private List<PartData> _partDataList;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        _partDataList = new List<PartData>();
    }

    public void SetBodyPart(PlayerBodyPartDataSO bodyPart)
    {
        BodyPart = bodyPart;
    }
    
    public void SetLegPart(PlayerLegPartDataSO legPart)
    {
        LegPart = legPart;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeAllPart();
        }
    }

    public void ChangeAllPart()
    {
        if (BodyPart == null || LegPart == null)
        {
            Debug.LogWarning("BodyPart or LegPart is null");
            return;
        }
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
    
    public void AddPartData(PlayerPartDataSO partData, bool isLoad = false)
    {
        if (PlayerPartDataList == null)
            PlayerPartDataList = new List<PlayerPartDataSO>();
        
        if (PlayerPartDataList.Contains(partData))
            return;
        PlayerPartDataList.Add(partData);
        if (isLoad)
            return;
        AddData(partData);
    }
    
    public void RemovePartData(PlayerPartDataSO partData)
    {
        if (PlayerPartDataList == null)
            return;
        if (!PlayerPartDataList.Contains(partData))
            return;
        PlayerPartDataList.Remove(partData);
    }

    private void AddData(PlayerPartDataSO partData)
    {
        _partDataList.Add(new PartData(partData.id, partData.partType));
    }

    public void SavePartData()
    {
        SaveManager.Instance.SaveToList(_partDataList, "PlayerPartData");
    }
    
    public void LoadPartData()
    {
        _partDataList = SaveManager.Instance.LoadFromList<PartData>("PlayerPartData");
        if (_partDataList == null)
            return;

        GivePart();
    }

    private void GivePart()
    {
        foreach (var partData in _partDataList)
        {
            var partDataSO = _partTable.playerPartDataSOList.Find
                (x => x.id == partData.partID &&
                      x.partType == partData.partType);
            if (PlayerPartDataList.Contains(partDataSO)) 
                continue;
            AddPartData(partDataSO, true);
        }
    }
    
    [ContextMenu("Test")]
    public void Test()
    {
        var bodyPart = _partTable.
            playerPartDataSOList.Find(
                x => x.partType == PartType.Body && x.id == 0);
        var legPart = _partTable.
            playerPartDataSOList.Find(
                x => x.partType == PartType.Leg && x.id == 0);
        var leg2 = _partTable.
            playerPartDataSOList.Find(
                x => x.partType == PartType.Leg && x.id == 1);
        AddPartData(bodyPart);
        AddPartData(legPart);
        AddPartData(leg2);
        SavePartData();
    }
    
    [ContextMenu("Reset")]
    public void Reseta()
    {
        _partDataList.Clear();
        PlayerPartDataList.Clear();
    }
    
    [ContextMenu("LoadTest")]
    public void LoadTest()
    {
        LoadPartData();
    }
}