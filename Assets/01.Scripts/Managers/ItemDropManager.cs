using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using ItemManage;
using ObjectPooling;
using UnityEngine;

public class ItemDropManager : MonoSingleton<ItemDropManager>
{
    [SerializeField] private List<ItemTableSO> _itemTableSOList;

    public Item DropItem(ItemType type, int id, Vector2 position)
    {
        ItemSO itemSO = FindItemSo(type, id);
        Item item = PoolingManager.Instance.Pop((PoolingType)(int)type) as Item;
        item.SetItem(itemSO);
        item.transform.position = position;
        return item;
    }
    
    public Item DropItem(ItemType type, int id, Vector2 startPosition, Vector2 endPosition)
    {
        ItemSO itemSO = FindItemSo(type, id);
        Item item = PoolingManager.Instance.Pop((PoolingType)(int)type) as Item;
        item.SetItem(itemSO);
        item.transform.position = startPosition;
        item.transform.DOJump(endPosition, 1.5f, 1, 1);
        return item;
    }

    private ItemSO FindItemSo(ItemType type, int id)
    {
        ItemTableSO itemTableSO = _itemTableSOList.Find(x => x.itemType == type);
        ItemSO itemSO = itemTableSO.itemSOList.Find(x => x.id == id);
        if(itemSO == null)
            Debug.LogWarning($"not exist ID : {id}");
        return itemSO;
    }
}
