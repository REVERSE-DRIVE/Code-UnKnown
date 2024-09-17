using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyManage {
    public class BossPillPieceTargetFollowState : EnemyState<PillPieceStateEnum>
    {
        PillPiece _agent;

        public BossPillPieceTargetFollowState(Enemy enemyBase, EnemyStateMachine<PillPieceStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
        {
            _agent = enemyBase as PillPiece;
        }

        public override void UpdateState()
        {
            base.UpdateState();

            if (Vector3.Distance(_agent.targetTrm.position, _agent.transform.position) <= _agent.attackDistance) {
                _enemyBase.MovementCompo.StopImmediately();
                return;
            }
    
            Vector3 dir = _agent.targetTrm.position - _agent.transform.position;
            _enemyBase.MovementCompo.SetMovement(dir.normalized);
        }
    }
}
