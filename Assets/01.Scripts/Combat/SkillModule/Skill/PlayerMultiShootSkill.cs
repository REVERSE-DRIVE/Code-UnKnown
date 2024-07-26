using System;
using System.Collections;
using System.Collections.Generic;
using ObjectManage;
using ObjectPooling;
using UnityEngine;

namespace CombatSkillManage
{
    public class PlayerMultiShootSkill : PlayerSkill
    {
        [SerializeField] private List<Transform> _targetMarkList;
        [SerializeField] private ParticleSystem _burstParticle;
        [SerializeField] private PoolingType _hitParticle;
        [Header("Setting Values")]
        [SerializeField] private int _damage;
        [SerializeField] private float _defaultRange = 5f;

        [SerializeField] private float _addRange; //레벨에 따라 증가되는 거리
        [SerializeField] private int _defaultTargetAmount = 3;
        
        private Collider2D[] _targets;
        private List<IDamageable> _hitList = new List<IDamageable>();
        private int TargetingAmount => level + _defaultTargetAmount;
        private int _targetAmount;
        private TrailRenderer _trailRenderer;
        private Transform _playerTrm;
        private Vector2 _beforePosition;

        private void Awake()
        {
            _trailRenderer = GetComponent<TrailRenderer>();
        }


        public override void Initialize(Player player)
        {
            base.Initialize(player);
            _playerTrm = player.transform;
            
        }


        public override void UseSkill()
        {
            _targets = new Collider2D[TargetingAmount];
            StartCoroutine(SkillCoroutine());

        }
        
        private IEnumerator SkillCoroutine()
        {
            
            DetectTargets();
            _beforePosition = transform.position;
            Time.timeScale = 0.4f;
            VolumeEffectManager.Instance.SetGrayScale(-80f, 0.4f, 0.1f);
            SetTargetMark();
            yield return new WaitForSeconds(0.2f);

            yield return StartCoroutine(AttackTargetsPosCoroutine());
            yield return StartCoroutine(AttackTargetsPosCoroutine());
            yield return new WaitForSeconds(0.2f);
            
            SetTargetMarksDisable();
            transform.position = _beforePosition;
            Time.timeScale = 1f;
            _burstParticle.Play();
            
            AttackBurstAllTargets();
        }

        private void DetectTargets()
        {
            _targetAmount = Physics2D.OverlapCircleNonAlloc(
                transform.position, _defaultRange + _addRange * level,
                _targets, _targetLayer);

            int newTargetAmount = 0;
            _hitList = new List<IDamageable>();
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

        private IEnumerator AttackTargetsPosCoroutine()
        {
            _trailRenderer.enabled = true;
            WaitForSeconds ws = new WaitForSeconds(0.03f);
            for (int i = 0; i < _targetAmount; i++)
            {
                _playerTrm.position = _targets[i].transform.position;
                yield return ws;
            }

            _trailRenderer.enabled = false;
        }

        private void AttackBurstAllTargets()
        {
            for (int i = 0; i < _hitList.Count; i++)
            {
                _hitList[i].TakeDamage(_damage);
                EffectObject effect = PoolingManager.Instance.Pop(_hitParticle) as EffectObject;
                effect.Initialize(_targets[i].transform.position);
                effect.Play();
            }
        }
    }
}