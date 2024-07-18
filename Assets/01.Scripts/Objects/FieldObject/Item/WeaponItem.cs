using ItemManage;
using UnityEngine;
using WeaponManage;

public class WeaponItem : Item
{
    [SerializeField] private WeaponInfoSO _weaponInfoSO;
    public override void SetItem(ItemSO itemSO)
    {
        base.SetItem(itemSO);
        _weaponInfoSO = itemSO.weaponInfoSO;
    }

    public override void Interact(InteractData data)
    {
        base.Interact(data);
        //PlayerManager.Instance.player.PlayerAttackCompo.ChangeWeapon(_weaponInfoSO);
    }
}