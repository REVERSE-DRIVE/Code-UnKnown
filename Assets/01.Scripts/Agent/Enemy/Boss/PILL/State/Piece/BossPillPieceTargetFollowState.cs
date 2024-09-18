using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyManage {
    public class BossPillPieceTargetFollowState : BossPillPieceLookState
    {
        PillPiece _agent;
        float rushTime = 0;

        public BossPillPieceTargetFollowState(Enemy enemyBase, EnemyStateMachine<PillPieceStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
        {
            _agent = enemyBase as PillPiece;
        }

        public override void UpdateState()
        {
            base.UpdateState();

            if (Vector3.Distance(_agent.targetTrm.position, _agent.transform.position) <= _agent.attackDistance) {
                _enemyBase.MovementCompo.StopImmediately();

                // 돌진 ㄱㄴ
                if (Time.time - rushTime > _agent.rushCooltime) {
                    rushTime = Time.time;
                    _stateMachine.ChangeState(PillPieceStateEnum.TargetRush);
                }
                return;
            }
    
            Vector3 dir = _agent.targetTrm.position - _agent.transform.position;
            _enemyBase.MovementCompo.SetMovement(dir.normalized);
        }
    }
}
