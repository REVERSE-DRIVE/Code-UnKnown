using System.Collections.Generic;
using UnityEngine;

namespace PlayerPartsManage
{
    [CreateAssetMenu(menuName = "SO/PlayerParts/PlayerPartTableSO")]
    public class PlayerPartTableSO : ScriptableObject
    {
        public PartType partType;
        public List<PlayerPartDataSO> playerPartDataSOList;
    }
}