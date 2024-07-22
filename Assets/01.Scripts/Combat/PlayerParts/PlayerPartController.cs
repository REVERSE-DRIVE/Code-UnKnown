using System.Collections.Generic;
using System.Linq;
using ButtonAttribute;
using UnityEngine;

namespace PlayerPartsManage
{
    public class PlayerPartController : MonoBehaviour
    {
        // 다리, 몸체, 볼트 순서 인덱싱
        public PlayerPart[] partList;
        public PlayerPartTableSO[] playerPartTableSO;
        
        [SerializeField] PlayerPartDataSO defaultPart;
        [SerializeField] PartType defaultPartType;
        
        [InspectorButton]
        public void InitParts()
        {
            ChangePart(defaultPartType, defaultPart);
        }
        
        public void SkillTrigger(PartType type)
        {
            partList[(int)type].UseSkill();
        }

        public void ChangePart(PartType type, PlayerPartDataSO anotherPart)
        {

            if (type == PartType.Body)
            {
                var sr = partList[(int)type].GetComponent<SpriteRenderer>();
                PlayerBodyPartDataSO bodyPartData = (PlayerBodyPartDataSO) anotherPart;
                sr.sprite = bodyPartData.bodyPartSprite;
            }
            else if (type == PartType.Leg)
            {
                var leftLeg = transform.Find("Visual").Find("Leg Left");
                var rightLeg = transform.Find("Visual").Find("Leg Right");
                Debug.Log(leftLeg);
                Debug.Log(rightLeg);
                List<SpriteRenderer> sr = leftLeg.GetComponentsInChildren<SpriteRenderer>().ToList();
                sr.AddRange(rightLeg.GetComponentsInChildren<SpriteRenderer>().ToList());
                
                PlayerLegPartDataSO legPartData = (PlayerLegPartDataSO) anotherPart;
                for (int i = 0; i < sr.Count; i++)
                {
                    sr[i].sprite = legPartData.legPartSprite[i];
                }
            }

        }
        
    }
}