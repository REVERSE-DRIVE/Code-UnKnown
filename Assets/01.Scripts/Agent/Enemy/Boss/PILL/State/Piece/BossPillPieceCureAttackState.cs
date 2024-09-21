using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyManage {
    public class BossPillPieceCureAttackState : BossPillPieceLookState
    {
        readonly int ANIM_ENDCUREATTACK = Animator.StringToHash("CureAttackEnd");
        enum Status { Fire, Wait, End }
        public BossPillPieceCureAttackState(Enemy enemyBase, EnemyStateMachine<PillPieceStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
        {
            
        }
        
        Status status;
        float timer;

        public override void Enter()
        {
            base.Enter();
            status = Status.Fire;
            timer = 0;
        }

        public override void UpdateState()
        {
            base.UpdateState();

            timer += Time.deltaTime;

            switch (status)
            {
                case Status.Fire:
                    ShotFire();
                    break;
                case Status.Wait:
                    WaitEnd();
                    break;
                case Status.End:
                    Finish();
                    break;
            }
        }

        private void Finish()
        {
            if (!_endTriggerCalled) return; // 아직 다 안닫힘..
            _stateMachine.ChangeState(PillPieceStateEnum.TargetFollow);
        }

        public override void Exit()
        {
            base.Exit();
            _agent.AnimatorCompo.SetBool(ANIM_ENDCUREATTACK, false);
        }

        private void WaitEnd()
        {
            if (timer < 0.5f) return; // 아직 안닫아
            
            _agent.AnimatorCompo.SetBool(ANIM_ENDCUREATTACK, true);
            status = Status.End;
        }

        void ShotFire() {
            if (timer < _agent.healBulletFireCooltime) return; // 아직 시간 안지남

            CureProjectile bullet = GameObject.Instantiate(_agent.cureBulletPrefab, _agent.firePos.position, Quaternion.identity);
            bullet.Shoot(_agent.targetTrm);

            status = Status.Wait;
            timer = 0;
        }
    }
}
