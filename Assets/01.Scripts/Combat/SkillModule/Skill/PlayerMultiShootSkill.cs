using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSkillManage
{
    public class PlayerMultiShootSkill : PlayerSkill
    {
        [SerializeField] private List<Transform> _targetMarkList;

        [Header("Setting Values")]
        [SerializeField] private float _defaultRange = 5f;

        [SerializeField] private float _addRange; //레벨에 따라 증가되는 거리
        [SerializeField] private int _defaultTargetAmount = 3;
        
        private Collider2D[] _targets;
        private List<IDamageable> _hitList;
        private int TargetingAmount => level + _defaultTargetAmount;
        private int _targetAmount;
        private TrailRenderer _trailRenderer;
        
        public override void UseSkill()
        {
            _targets = new Collider2D[TargetingAmount];
            StartCoroutine(SkillCoroutine());

        }

        private void DetectTargets()
        {
            _targetAmount = Physics2D.OverlapCircleNonAlloc(
                transform.position, _defaultRange + _addRange * level,
                _targets, _targetLayer);

            int newTargetAmount = 0;
            for (int i = 0; i < _targetAmount; i++)
            {
                if (_targets[i].TryGetComponent(out IDamageable hit))
                {
                    _hitList.Add(hit);
                    _targets[newTargetAmount] = _targets[i];
                    newTargetAmount++;
                }
            }

            _targetAmount = newTargetAmount;
        }

        private IEnumerator SkillCoroutine()
        {
            
            DetectTargets();
            Time.timeScale = 0.2f;
            SetTargetMark();
            yield return new WaitForSeconds(0.1f);

            SetTargetMarksDisable();
            Time.timeScale = 1f;
        }

        private void SetTargetMark()
        {
            CheckTargetMarkAmount();
            SetTargetMarksActive();
        }

        private void SetTargetMarksActive()
        {
            for (int i = 0; i < _targetAmount; i++)
            {
                _targetMarkList[i].gameObject.SetActive(true);
                _targetMarkList[i].position = _targets[i].transform.position;
            }
        }
        
        private void SetTargetMarksDisable()
        {
            for (int i = 0; i < _targetMarkList.Count; i++)
            {
                _targetMarkList[i].gameObject.SetActive(false);
            }
        }

        private void CheckTargetMarkAmount()
        {
            if (_targetMarkList.Count < TargetingAmount)
            {
                int need = TargetingAmount - _targetMarkList.Count; 
                for (int i = 0; i < need; i++)
                {
                    _targetMarkList.Add(Instantiate(_targetMarkList[i]));
                }
            }
            
            
        }
        
        
    }
}