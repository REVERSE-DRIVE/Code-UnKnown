using System.Collections;
using System.Collections.Generic;
using EnemyManage;
using UnityEngine;

namespace EnemyManage {
    public class BossPillBodyIdleState : EnemyState<PillBodyStateEnum>
    {
        PillBody agent;
        float waitTimer;
        
        public BossPillBodyIdleState(Enemy enemyBase, EnemyStateMachine<PillBodyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
        {
            agent = enemyBase as PillBody;
            ResetTimer();
        }

        public override void UpdateState()
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0) { // 스킬 써야지ㅣㅣ

                int idx = Random.Range(0, agent.useSkills.Length + agent.usePieceSkills.Length);
                
                if (idx <= agent.useSkills.Length - 1) {
                    PillBodyStateEnum state = agent.useSkills[idx];
                    _stateMachine.ChangeState(state);
                } else {
                    PillPieceStateEnum state = agent.usePieceSkills[agent.useSkills.Length - idx];
                    agent.AllChangeState(state);
                }

                ResetTimer();
            }
        }

        void ResetTimer() {
            waitTimer = Random.Range(agent.skillUseTime.x, agent.skillUseTime.y);
        }
    }
}