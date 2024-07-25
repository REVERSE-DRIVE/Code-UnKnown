using UnityEngine;
using UnityEngine.UI;

namespace CombatSkillManage
{
    
    public class PlayerSkillController : MonoBehaviour
    {
        [SerializeField] private PlayerSkillSO _currentSkillSO;
        [SerializeField] private Image _skillIconImage;
        [SerializeField] private Image _skillCooltimeGaugeImage;
        
        private PlayerSkill _currentSkill;
        private Player _player;

        [SerializeField] private float _currentTime;
        [SerializeField] private bool _canUseSkill;
        
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
            _currentTime += Time.deltaTime * _currentSkill.coolingPower;
            Refresh();
        }

        public void Initialize()
        {
            _currentSkill = Instantiate(_currentSkillSO.skillPrefab, transform);
            _currentSkill.Initialize(_player);
            _skillIconImage.sprite = _currentSkillSO.skillIcon;
            
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