using System;
using ObjectManage;
using TMPro;
using UnityEngine;

namespace ItemManage
{
    public class Item : InteractObject
    {
        [field:SerializeField] public ItemSO ItemSO { get; private set; }
        public TextMeshPro _itemNameText;

        public void SetItem(ItemSO itemSO)
        {
            ItemSO = itemSO;
            _itemNameText.text = ItemSO.itemName;
            _visualRenderer.sprite = ItemSO.itemIcon;
        }
    }
}