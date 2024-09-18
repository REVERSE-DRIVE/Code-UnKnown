using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyManage {
    public class BossPillPieceLookState : EnemyState<PillPieceStateEnum>
    {
        protected PillPiece _agent;
        public BossPillPieceLookState(Enemy enemyBase, EnemyStateMachine<PillPieceStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
        {
            _agent = enemyBase as PillPiece;
        }

        public override void UpdateState()
        {
            base.UpdateState();
            
            Vector3 dir = _agent.targetTrm.position - _agent.transform.position;
            float rotationZ = Mathf.Acos(dir.x / dir.magnitude) * 180 / Mathf.PI * Mathf.Sign(dir.y);
            
            _agent.transform.rotation = Quaternion.Lerp(_agent.transform.rotation, Quaternion.Euler(0, 0, rotationZ + 90), Time.deltaTime * _agent.lookSpeed);
        }
    }
}