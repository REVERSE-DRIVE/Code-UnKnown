using UnityEngine;
using WeaponManage;

public class TestGun : Gun
{
    protected override void AttackLogic()
    {
        Debug.Log("A");
    }
}