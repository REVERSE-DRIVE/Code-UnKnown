using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyManage {
    public class BossPillPieceTargetFollowState : EnemyState<PillPieceStateEnum>
    {
        public BossPillPieceTargetFollowState(Enemy enemyBase, EnemyStateMachine<PillPieceStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
        {
        }

        
    }
}
