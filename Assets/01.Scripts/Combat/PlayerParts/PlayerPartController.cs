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
        
        [ContextMenu("TestSkillTrigger")]
        public void TestChangePart()
        {
            ChangePart(defaultPartType, defaultPart);
        }
        
        public void MountTrigger(PartType type)
        {
            partList[(int)type].OnMount();
        }
        
        public void UnMountTrigger(PartType type)
        {
            partList[(int)type].OnUnMount();
        }

        public void ChangePart(PartType type, PlayerPartDataSO anotherPart)
        {
            UnMountTrigger(type);
            Destroy(partList[(int)type].gameObject);
            partList[(int)type] = Instantiate(anotherPart.partPrefab, _visualTrm);
            MountTrigger(type);
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