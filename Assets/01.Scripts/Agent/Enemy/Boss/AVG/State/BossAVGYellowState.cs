using System.Collections;
using UnityEngine;

namespace EnemyManage
{
    public class BossAVGYellowState : BossAVGState
    {
        private float _stateDuration;
        private float _currentTime = 0;
        private int _selfHitCount = 0;
        private int _selfHitLimit = 3; // 자해 데미지 한계
        private Transform _rangeTrm;
        private SpriteRenderer _rangeRenderer;
        private Material _rangeMaterial;
        private int _blinkHash;
        private Player _player;
        private Transform _playerTrm;
        private float _detectRange = 50f;
        private float _attackInterval;
        private LayerMask _playerLayer;
        private int _attackAmount;
        private Coroutine _stateCoroutine;
        private WaitForSeconds waitSec;
        
        public BossAVGYellowState(Enemy enemyBase, EnemyStateMachine<AVGStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
        {
           
        }

        public override void Enter()
        {
            base.Enter();
            _bossAVGBase._isResist = true;
            
            _blinkHash = Shader.PropertyToID("_UseBlink");
            _rangeTrm = _bossAVGBase._yellowAttackRangeTrm;
            _rangeRenderer = _bossAVGBase._yellowRangeRenderer;
            _rangeMaterial = _bossAVGBase._rangeMaterial;
            _playerLayer = _bossAVGBase._yellowAttackLayer;
            _attackAmount = _bossAVGBase._attackAmount;
            _attackInterval = _bossAVGBase._attackInterval;
            _selfHitCount = 0;
            waitSec = new WaitForSeconds(1f);

            _stateCoroutine = _bossAVGBase.StartCoroutine(StateCoroutine());
        }

        
        private IEnumerator StateCoroutine()
        {
            yield return waitSec;
            WaitForSeconds ws = new WaitForSeconds(_attackInterval);
            for (int i = 0; i < _attackAmount; i++)
            {
                yield return _bossAVGBase.StartCoroutine(AttackCoroutine());
                yield return ws;
            }

            yield return waitSec;
            _bossAVGBase.StateMachine.ChangeState(AVGStateEnum.Idle);
        }

        

        private IEnumerator AttackCoroutine()
        {
            if (!DetectTarget())
            {
                yield break;
            }

            _rangeTrm.position = _playerTrm.position;
            _rangeRenderer.enabled = true;
            _rangeMaterial.SetInt(_blinkHash, 1);
            yield return new WaitForSeconds(1f);
            _rangeMaterial.SetInt(_blinkHash, 0);
            DamageBurst();
            _bossAVGBase.AVGVFXCompo.PlayYellowImpact();
            _rangeRenderer.enabled = false; 
        }

        private bool DetectTarget()
        {
            Collider2D hit = Physics2D.OverlapCircle(_bossAVGBase.transform.position, _detectRange, _playerLayer);
            if (hit == null)
            {
                return false;
            }
            if (hit.TryGetComponent(out Player player))
            {
                _player = player;
                _playerTrm = player.transform;
                return true;
            }

            return true;
        }

        private void DamageBurst()
        {
            Collider2D hit = Physics2D.OverlapCircle(_rangeTrm.position, _bossAVGBase._attackRadius, _playerLayer);
            if (hit == null)
            {
                return;
            }
            if (hit.TryGetComponent(out IDamageable health))
            {
                health.TakeDamage(_bossAVGBase._yellowBurstDamage);
            }
            
            if (hit.TryGetComponent(out IStrongDamageable strongHealth))
            {
                strongHealth.TakeStrongDamage(_bossAVGBase._yellowBurstDamage);
            }

            if (hit.TryGetComponent(out AgentMovement movement))
            {
                movement.GetKnockBack((hit.transform.position - _rangeTrm.position).normalized * 40f, 0.1f);
            }
        }

        public override void Exit()
        {
            base.Exit();
            if(_stateCoroutine != null)
                _bossAVGBase.StopCoroutine(_stateCoroutine);
        }

        public override void CustomTrigger()
        {
            _selfHitCount++;
            Debug.Log("여기서 스턴이 먹어야되는데???");
            Debug.Log($"<color=yellow>Self Hit Count : {_selfHitCount}</color>");
            if (_selfHitCount >= _selfHitLimit)
            {
                _stateMachine.ChangeState(AVGStateEnum.Stun);
            }

        }
    }
}