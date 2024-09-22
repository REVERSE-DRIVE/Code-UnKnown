using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyManage {
    public class BossPillPieceLeaserAttackState : EnemyState<PillPieceStateEnum>
    {
        PillPiece agent;
        IDamageable damageable;
        float timer;

        public BossPillPieceLeaserAttackState(Enemy enemyBase, EnemyStateMachine<PillPieceStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
        {
            agent = enemyBase as PillPiece;
        }

        public override void Enter()
        {
            base.Enter();
            agent.Leaser.SetActive(true);
            timer = 0;
            damageable = agent.targetTrm.GetComponent<IDamageable>();
        }

        public override void UpdateState()
        {
            timer -= Time.deltaTime;
            if (timer > 0 || !agent.Leaser.IsTargetDetect) return;
            
            timer = agent.leaserCooltime;
            damageable.TakeDamage(agent.leaserDamage);
        }

        public override void Exit()
        {
            base.Exit();
            agent.Leaser.SetActive(false);
        }
    }
}
