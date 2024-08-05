using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/BossInfo/List")]
public class BossInfoListSO : ScriptableObject
{
    public List<BossInfoSO> list;

    public BossInfoSO Find(int id)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].id == id)
                return list[i];
        }
        return null;
    }
}