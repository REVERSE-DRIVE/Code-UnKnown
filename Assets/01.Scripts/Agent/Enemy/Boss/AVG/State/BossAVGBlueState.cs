using System.Collections;
using UnityEngine;

namespace EnemyManage
{

    public class BossAVGBlueState : BossAVGState
    {
        private int _damage;
        private float _attackCoolTime = 0.2f;
        private float _stateDuration ;
        private int _projectileAmount;
        private float _attackRadius;
        private LayerMask _targetLayer;
        private int _strongHitCount = 0; // 집파일 공격 피격시 쌓임

        private float _currentStateTime = 0;
        private float _currentTime = 0;
        
        
        private Collider2D[] _targets = new Collider2D[5];
        private Vector2 _targetPos;
        private Player _player;
        private Vector2 _direction;
        
        private Coroutine _coroutine;
        private Coroutine _stateCoroutine;
        private Transform _bossTrm;
        
        public BossAVGBlueState(Enemy enemyBase, EnemyStateMachine<AVGStateEnum> stateMachine,
            string animBoolName) : base(
            enemyBase, stateMachine, animBoolName)
        {
        }


        public override void Enter()
        {
            base.Enter();
            _attackCoolTime = _bossAVGBase._attackCooltime;
            _currentStateTime = 0;
            _player = PlayerManager.Instance.player;
            _attackRadius = _bossAVGBase._spinAttackRadius;
            _targetLayer = _bossAVGBase._spinAttackTargetLayer;
            _damage = _bossAVGBase._spinAttackDamage;
            _stateDuration = _bossAVGBase._attackDuration;
            _bossTrm = _bossAVGBase.transform;
            _strongHitCount = 0;
            _stateCoroutine = _bossAVGBase.StartCoroutine(StateCoroutine());
        }

        public override void UpdateState()
        {
            base.UpdateState();
            _currentTime += Time.deltaTime;
            _currentStateTime += Time.deltaTime;

            if (_currentTime >= _attackCoolTime)
            {
                _currentTime = 0;
                CastDamage();
            }

            if (_currentStateTime >= _stateDuration)
            {
                _stateMachine.ChangeState(AVGStateEnum.Idle);
            }

        }

        public override void Exit()
        {
            base.Exit();
            if(_coroutine != null)
                _bossAVGBase.StopCoroutine(_coroutine);
            if(_stateCoroutine != null)
                _bossAVGBase.StopCoroutine(_stateCoroutine);
            
            _bossAVGBase.AVGVFXCompo.StopSpinVFX();
        }

        
        private IEnumerator StateCoroutine()
        {
            while (_currentStateTime < _stateDuration)
            {
                _coroutine = _bossAVGBase.StartCoroutine(DashCoroutine());
                yield return _coroutine;
                yield return new WaitForSeconds(1f);
            }
            _bossAVGBase.ResetPosition();
            
        }

        private IEnumerator DashCoroutine()
        {
            Vector2 beforePos = _bossTrm.position;
            _targetPos = _player.transform.position;
            _bossAVGBase.AVGVFXCompo.PlaySpinVFX();
            float currentTime = 0;
            float duration = 0.1f + (_targetPos - beforePos).magnitude / 20f;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                _bossTrm.position = Vector2.Lerp(beforePos, _targetPos, currentTime / duration);
                yield return null;
            }
            _bossTrm.position = _targetPos;
            _bossAVGBase.AVGVFXCompo.StopSpinVFX();
        }

        private void CastDamage()
        {
            int amount = Physics2D.OverlapCircleNonAlloc(_bossAVGBase.transform.position, _attackRadius, _targets,
                _targetLayer);
            if (amount == 0) return;
            for (int i = 0; i < amount; i++)
            {
                if (_targets[i].TryGetComponent(out IDamageable hit))
                {
                    hit.TakeDamage(_damage);
                }
            }
        }


        public override void CustomTrigger()
        {
            _strongHitCount++;
            if (_strongHitCount > _bossAVGBase._stunNeedHitCount)
            {
                _stateMachine.ChangeState(AVGStateEnum.Stun);
            }

        }
    }
}