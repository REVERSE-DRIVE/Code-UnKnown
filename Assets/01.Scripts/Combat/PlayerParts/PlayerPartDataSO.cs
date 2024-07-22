using UnityEngine;

namespace PlayerPartsManage
{
    
    public class PlayerPartDataSO : ScriptableObject
    {
        // 이를 상속받아서 구현한다.
        public PartType partType;
        public int id;
        public string partName;
        public string description;
        public PlayerPart partPrefab;
    }
}