using System.Collections.Generic;
using UnityEngine;

namespace PlayerPartsManage
{
    public class PlayerPartController : MonoBehaviour
    {
        // 다리, 몸체, 볼트 순서 인덱싱
        public PlayerPart[] partList;
        
        public void SkillTrigger(PartType type)
        {
            partList[(int)type].UseSkill();
        }

        public void ChangePart(PartType type, PlayerPartDataSO anotherPart)
        {
            // 플레이어 파츠 변경을 구현 해야한다}

        }
    }
}