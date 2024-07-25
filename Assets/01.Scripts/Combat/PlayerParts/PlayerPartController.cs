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
        
        [SerializeField] private SpriteRenderer _bodySpriteRenderer;
        [SerializeField] private SpriteRenderer[] _legSpriteRenderers;
        
        [SerializeField] PlayerPartDataSO defaultPart;
        [SerializeField] PartType defaultPartType;
        
        private void Awake()
        {
            _visualTrm = transform.Find("Visual");
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
            Destroy(partList[(int)type].gameObject);
            partList[(int)type] = Instantiate(anotherPart.partPrefab, _visualTrm);
            if (type == PartType.Body)
                ChangeSprite(anotherPart as PlayerBodyPartDataSO);
            else if (type == PartType.Leg)
                ChangeSprite(anotherPart as PlayerLegPartDataSO);
        }

        private void ChangeSprite(PlayerLegPartDataSO anotherPart)
        {
            for (int i = 0; i < _legSpriteRenderers.Length; i++)
            {
                _legSpriteRenderers[i].sprite = anotherPart.legPartSprites[i];
            }
        }

        private void ChangeSprite(PlayerBodyPartDataSO anotherPart)
        {
            _bodySpriteRenderer.sprite = anotherPart.bodyPartSprite;
        }
    }
}