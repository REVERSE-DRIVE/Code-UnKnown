using ItemManage;
using UnityEngine;

public class ResourceItem : Item
{
    [SerializeField] private ResourceRank _resourceRank;
    
    public override void SetItem(ItemSO itemSO)
    {
        base.SetItem(itemSO);
        _resourceRank = itemSO.resourceRank;
    }

    public override void Interact(InteractData data)
    {
        if (_isInteracted) return;
        base.Interact(data);
        LevelManager.Instance.ApplyExp((int)_resourceRank);
    }
}