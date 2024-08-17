using UnityEngine;

public class SlamShopPanel : WindowUI
{
    [SerializeField] private SlamShopItem _slamShopItemPrefab;
    [SerializeField] private Transform _content;
    [field:SerializeField] public PartWindow partWindow { get; set; }
}