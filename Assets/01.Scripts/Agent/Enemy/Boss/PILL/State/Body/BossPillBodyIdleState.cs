using System.Collections;
using System.Collections.Generic;
using EnemyManage;
using UnityEngine;

namespace EnemyManage {
    public class BossPillBodyIdleState : EnemyState<PillBodyStateEnum>
    {
        PillBody agent;
        public BossPillBodyIdleState(Enemy enemyBase, EnemyStateMachine<PillBodyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
        {
            agent = enemyBase as PillBody;
        }
    }
}