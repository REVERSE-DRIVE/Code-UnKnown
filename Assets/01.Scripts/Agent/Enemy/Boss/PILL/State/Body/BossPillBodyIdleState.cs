using System.Collections;
using System.Collections.Generic;
using EnemyManage;
using UnityEngine;

namespace EnemyManage {
    public class BossPillBodyIdleState : EnemyState<PillBodyStateEnum>
    {
        PillBody agent;
        float waitTimer;
        bool useCureWave = false; // 피가 없을떄 쓰는 스킬 썻나?
        
        public BossPillBodyIdleState(Enemy enemyBase, EnemyStateMachine<PillBodyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
        {
            agent = enemyBase as PillBody;
            ResetTimer();
        }

        public override void UpdateState()
        {
            waitTimer -= Time.deltaTime;

            // 피가 너무 적은가???
            if (!useCureWave && agent.HealthCompo.CurrentHealth <= agent.HealthCompo.maxHealth * agent.cureWaveRunHealth) {
                useCureWave = true;
                _stateMachine.ChangeState(PillBodyStateEnum.CureWave);
            }

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