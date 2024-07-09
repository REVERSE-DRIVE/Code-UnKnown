using System;
using System.Collections;
using System.Collections.Generic;
using ItemManage;
using UnityEngine;

public class ItemDropManager : MonoSingleton<ItemDropManager>
{
    [SerializeField] private Item _itemPrefab;
    [SerializeField] private List<ItemTableSO> _itemTableSOList;

    public Item DropItem(ItemType type, int id, Vector2 position)
    {
        ItemSO itemSO = FindItemSo(type, id);
        Item item = Instantiate(_itemPrefab, position, Quaternion.identity);
        item.SetItem(itemSO);
        return item;
    }

    private ItemSO FindItemSo(ItemType type, int id)
    {
        ItemTableSO itemTableSO = _itemTableSOList.Find(x => x.itemType == type);
        ItemSO itemSO = itemTableSO.itemSOList.Find(x => x.id == id);
        return itemSO;
    }
}
