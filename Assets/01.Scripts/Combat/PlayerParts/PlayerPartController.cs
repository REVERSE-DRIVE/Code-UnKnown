using UnityEngine;

namespace PlayerPartsManage
{
    public class PlayerPartController : MonoBehaviour
    {
        // 다리, 몸체, 볼트 순서 인덱싱
        public PlayerPart[] partList;
        public PlayerPartTableSO[] playerPartTableSO;
        
        private Transform _visualTrm;
        public PlayerBodyPartDataSO CurrentBodyPart { get; private set; }
        public PlayerLegPartDataSO CurrentLegPart { get; private set; }
        
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
            print("파츠를 바꾸");
            UnMountTrigger(type);
            Destroy(partList[(int)type].gameObject);
            partList[(int)type] = Instantiate(anotherPart.partPrefab, _visualTrm);
            MountTrigger(type);
            if (type == PartType.Body)
            {
                PlayerBodyPartDataSO part = anotherPart as PlayerBodyPartDataSO;
                ChangeSprite(part);
                CurrentBodyPart = part;
            }
            else if (type == PartType.Leg)
            {
                PlayerLegPartDataSO part = anotherPart as PlayerLegPartDataSO;
                ChangeSprite(part);
                CurrentLegPart = part;
            }
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