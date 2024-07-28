using System.Collections;
using UnityEngine;

namespace CombatSkillManage
{
    public class PlayerBerserkSkill : PlayerSkill
    {
        [Header("Setting Values")]
        [SerializeField] private float _defaultDuration = 10f;
        [SerializeField] private float _statusMultipleValue;

        private int _speedIncValue;
        private int _dashIncValue;
        private bool _isActive;
        public override void UseSkill()
        {
            if (_isActive) return;

            StartCoroutine(SkillCoroutine());
        }

        private IEnumerator SkillCoroutine()
        {
            _speedIncValue = _player.Stat.moveSpeed.GetValue();
            _dashIncValue = _player.additionalStat.dashSpeed.GetValue();
            yield return new WaitForSeconds(_defaultDuration + level);
            EndSkill();
        }

        private void EndSkill()
        {
            _player.Stat.RemoveModifier(StatType.MoveSpeed, _speedIncValue);
        }
        
    }
}