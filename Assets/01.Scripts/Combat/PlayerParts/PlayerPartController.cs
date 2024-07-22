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
        
        private Transform _visualTrm;
        private Transform _bodyTrm;
        private Transform _legLeftTrm;
        private Transform _legRightTrm;
        
        [SerializeField] PlayerPartDataSO defaultPart;
        [SerializeField] PartType defaultPartType;
        
        private void Awake()
        {
            _visualTrm = transform.Find("Visual");
            _bodyTrm = _visualTrm.Find("Body");
            _legLeftTrm = _visualTrm.Find("Leg Left");
            _legRightTrm = _visualTrm.Find("Leg Right");
        }
        
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
                SpriteRenderer sr = _bodyTrm.GetComponent<SpriteRenderer>();
                PlayerBodyPartDataSO bodyPartData = (PlayerBodyPartDataSO) anotherPart;
                sr.sprite = bodyPartData.bodyPartSprite;
            }
            else if (type == PartType.Leg)
            {
                SpriteRenderer[] sr = GetSpriteRenderers(_legLeftTrm, _legRightTrm);
                PlayerLegPartDataSO legPartData = (PlayerLegPartDataSO) anotherPart;
                for (int i = 0; i < sr.Length; i++)
                {
                    sr[i].sprite = legPartData.legPartSprite[i];
                }
            }
            
            Destroy(partList[(int)type].gameObject);
            partList[(int)type] = Instantiate(anotherPart.partPrefab, _visualTrm);
            
        }
        
        private SpriteRenderer[] GetSpriteRenderers(params Transform[] trms)
        {
            List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();
            foreach (var trm in trms)
            {
                spriteRenderers.AddRange(trm.GetComponentsInChildren<SpriteRenderer>());
            }

            return spriteRenderers.ToArray();
        }
        
    }
}