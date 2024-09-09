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

    protected override void Awake()
    {
        //base.Awake();
        DontDestroyOnLoad(this);
        _partDataList = new List<PartData>();
    }

    private void Start()
    {
        // for (int i = 0; i < PlayerPartDataList.Count; i++)
        // {
        //     AddData(PlayerPartDataList[i]);
        // }
        LoadPartData();
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
        if (partType == PartType.Body)
            SetBodyPart(partData as PlayerBodyPartDataSO);
        else if (partType == PartType.Leg)
            SetLegPart(partData as PlayerLegPartDataSO);
    }
    
    public void AddPartData(int id, PartType type, bool isLoad = false)
    {
        if (PlayerPartDataList == null)
            PlayerPartDataList = new List<PlayerPartDataSO>();
        var partData = _partTable.playerPartDataSOList
            .Find(x => x.id == id && x.partType == type);
        
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
            AddPartData(partDataSO.id, partDataSO.partType, true);
        }
    }
    
    public int GetPartCount(int id, PartType type)
    {
        return PlayerPartDataList.FindAll(x => x.id == id && x.partType == type).Count;
    }


    [ContextMenu("Test1")]
    public void Give()
    {
        AddPartData(0, PartType.Body);
        AddPartData(0, PartType.Body);
        AddPartData(1, PartType.Body);
        AddPartData(0, PartType.Leg);
        AddPartData(1, PartType.Leg);
    }
    
    [ContextMenu("Test2")]
    public void Save()
    {
        SavePartData();
    }
    
    [ContextMenu("Test3")]
    public void Load()
    {
        LoadPartData();
    }
    
    [ContextMenu("Test4")]
    public void Clear()
    {
        PlayerPartDataList.Clear();
        _partDataList.Clear();
    }
}