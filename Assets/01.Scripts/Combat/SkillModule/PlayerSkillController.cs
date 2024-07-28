using UnityEngine;
using UnityEngine.UI;

namespace CombatSkillManage
{
    public class PlayerSkillController : MonoBehaviour
    {
        [SerializeField] private PlayerSkillSO _currentSkillSO;
        private PlayerSkill _currentSkill;
        private SkillRecoveryType _skillRecoveryType;
        [SerializeField] private Image _skillIconImage;
        [SerializeField] private Image _skillCooltimeGaugeImage;
        [SerializeField] private float _currentTime;
        [SerializeField] private bool _canUseSkill;
        
        private Player _player;

        public bool IsCoolDown => _currentTime >= _currentSkillSO.coolTime;
        
        private void Start()
        {
            _player = PlayerManager.Instance.player;
            _player.PlayerInputCompo.controlButtons.OnSkillEvent += HandleUseSkill;
            
            Initialize();
        }

        private void Update()
        {
            if (_currentSkill == null) return;
            if (_skillRecoveryType == SkillRecoveryType.Normal)
            {
                HandleCooling();
            }
                
            
            Refresh();
        }

        public void Initialize()
        {
            _currentSkill = Instantiate(_currentSkillSO.skillPrefab, transform);
            _currentSkill.Initialize(_player);
            _skillIconImage.sprite = _currentSkillSO.skillIcon;
            _skillRecoveryType = _currentSkillSO.skillRecoveryType;

            switch (_skillRecoveryType)
            {
                case SkillRecoveryType.Attack:
                    _player.PlayerAttackCompo.OnAttackEvent += HandleCoolingInt;
                    break;
                
                case SkillRecoveryType.Hit:
                    _player.HealthCompo.OnHealthChangedEvent.AddListener(HandleCoolingInt);
                    break;
            }
        }

        private void HandleCooling()
        {
            _currentTime += Time.deltaTime * _currentSkill.coolingPower;
        }

        private void HandleCoolingInt()
        {
            _currentTime += _currentSkill.coolingPower;
        }
        
        

        public void HandleUseSkill()
        {
            print("ming");
            if (!_canUseSkill) return;
            if (!IsCoolDown) return;
            _currentTime = 0;
            _currentSkill.UseSkill();
        }

        private void Refresh()
        {
            _skillCooltimeGaugeImage.fillAmount = Mathf.Clamp01(_currentTime / _currentSkillSO.coolTime);
        }


    }

}