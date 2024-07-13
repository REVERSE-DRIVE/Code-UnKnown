using System.Collections.Generic;
using UnityEngine;

namespace WeaponManage
{
    public class WeaponInfoListSO : ScriptableObject
    {
        public List<WeaponInfoSO> list;

        public WeaponInfoSO FindWeapon(int id)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].id == id)
                {
                    return list[i];
                }
            }
            return null;
        }
        
    }
}