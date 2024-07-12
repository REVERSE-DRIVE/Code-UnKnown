using System;
using System.Collections;
using System.Collections.Generic;
using ItemManage;
using ObjectPooling;
using UnityEngine;

public class ItemDropManager : MonoSingleton<ItemDropManager>
{
    [SerializeField] private List<ItemTableSO> _itemTableSOList;

    public Item DropItem(ItemType type, int id, Vector2 position)
    {
        ItemSO itemSO = FindItemSo(type, id);
        Item item = PoolingManager.Instance.Pop(PoolingType.ItemBase) as Item;
        item.SetItem(itemSO);
        item.transform.position = position;
        return null;
    }

    private ItemSO FindItemSo(ItemType type, int id)
    {
        ItemTableSO itemTableSO = _itemTableSOList.Find(x => x.itemType == type);
        ItemSO itemSO = itemTableSO.itemSOList.Find(x => x.id == id);
        return itemSO;
    }
}
