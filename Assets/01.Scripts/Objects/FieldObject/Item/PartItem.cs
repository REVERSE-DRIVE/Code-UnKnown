using System.Collections;
using System.Collections.Generic;
using ItemManage;
using PlayerPartsManage;
using UnityEngine;

public class PartItem : Item
{
    [SerializeField] private PlayerPartDataSO _playerPartDataSO;

    public override void SetItem(ItemSO itemSO)
    {
        base.SetItem(itemSO);
        _playerPartDataSO = itemSO.partDataSO;
    }

    public override void Interact(InteractData data)
    {
        base.Interact(data);
        PlayerPartManager.Instance.ChangePart(_playerPartDataSO.partType, _playerPartDataSO);
    }
}
