using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyManage {
    public class BossPillPieceCureAttackState : BossPillPieceLookState
    {
        readonly int ANIM_ENDCUREATTACK = Animator.StringToHash("CureAttackEnd");
        enum Status { Follow, Wait, End }
        public BossPillPieceCureAttackState(Enemy enemyBase, EnemyStateMachine<PillPieceStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
        {
            
        }
        
        Status status;

        float timer;
        float fireTimer;

        IEnumerator processHandler;

        public override void Enter()
        {
            base.Enter();
            timer = 0;
            fireTimer = 0;
            status = Status.Follow;
        }

        public override void UpdateState()
        {
            base.UpdateState(); // 비활하면 안봄
            timer += Time.deltaTime;
            
            if (status == Status.Wait) return;
            if (status == Status.End) { // 애니메이션 끝날때까지 대기...
                if (_endTriggerCalled)
                    _stateMachine.ChangeState(PillPieceStateEnum.TargetFollow);
                return;   
            }

            if (timer > _agent.cureAttackDuration) {
                status = Status.End;
                _agent.AnimatorCompo.SetBool(ANIM_ENDCUREATTACK, true);
                return;
            }

            fireTimer -= Time.deltaTime;
            if (fireTimer <= 0) { // 쏴도됨
                status = Status.Wait;
                processHandler = ShootCoroutine();

                _enemyBase.MovementCompo.StopImmediately();
                _agent.StartCoroutine(processHandler);
                return;
            }

            if (Vector3.Distance(_agent.targetTrm.position, _agent.transform.position) <= _agent.attackDistance) return;
            Vector3 dir = _agent.targetTrm.position - _agent.transform.position;
            _enemyBase.MovementCompo.SetMovement(dir.normalized);
        }

        public override void Exit()
        {
            base.Exit();

            // 아름다운 사람은 머무른 자리도 아름답다. - 도미 -
            if (processHandler != null) {
                _agent.StopCoroutine(processHandler);
                processHandler = null;
            }

            _agent.AnimatorCompo.SetBool(ANIM_ENDCUREATTACK, false);
        }

        IEnumerator ShootCoroutine() {
            yield return new WaitForSeconds(1);
            fireTimer = _agent.healBulletFireCooltime; // 쿨탐 지정
            
            Vector3 dir = _agent.targetTrm.position - _agent.transform.position;

            CureProjectile bullet = GameObject.Instantiate(_agent.cureBulletPrefab, _agent.firePos.position, Quaternion.identity);
            bullet.Shoot(dir.normalized);

            yield return new WaitForSeconds(0.5f);
            status = Status.Follow;
        }
    }
}
