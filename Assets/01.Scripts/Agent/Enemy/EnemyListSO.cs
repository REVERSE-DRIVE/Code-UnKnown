using System.Collections.Generic;
using UnityEngine;

namespace EnemyManage
{
    [CreateAssetMenu(fileName = "EnemyListSO", menuName = "SO/Enemy/EnemyListSO")]
    public class EnemyListSO : ScriptableObject
    {
        public List<EnemySO> list;
    }
}