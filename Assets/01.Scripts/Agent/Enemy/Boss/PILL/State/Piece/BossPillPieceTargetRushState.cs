using UnityEngine;

namespace EnemyManage {
    public class BossPillPieceTargetRushState : EnemyState<PillPieceStateEnum>
    {
        PillPiece agent;

        public BossPillPieceTargetRushState(Enemy enemyBase, EnemyStateMachine<PillPieceStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
        {
            agent = enemyBase as PillPiece;
        }
        
        bool alreadyHit;
        Vector3 rushDir;
        float timer;
        Collider2D[] colliders = new Collider2D[1];

        public override void Enter()
        {
            base.Enter();

            timer = 0;
            alreadyHit = false;

            // 방향 정하기
            rushDir = (agent.targetTrm.position - agent.transform.position).normalized;
            agent.Stat.AddModifier(StatType.MoveSpeed, agent.rushSpeed);
        }

        public override void UpdateState()
        {
            timer += Time.deltaTime;
            if (timer > agent.rushDuration) {
                agent.MovementCompo.StopImmediately();
                _stateMachine.ChangeState(PillPieceStateEnum.TargetFollow);
                return;
            }
            
            if (!alreadyHit) {
                int count = Physics2D.OverlapCircleNonAlloc(agent.transform.position, (agent.ColliderCompo as CircleCollider2D).radius, colliders, agent.WhatIsPlayer);
                if (count > 0 && colliders[0].transform.TryGetComponent(out IDamageable damageable)) {
                    alreadyHit = true;
                    damageable.TakeDamage(agent.rushDamage);
                }
            }
            
            agent.MovementCompo.SetMovement(rushDir);
        }

        public override void Exit()
        {
            base.Exit();
            agent.MovementCompo.StopImmediately();
            agent.Stat.RemoveModifier(StatType.MoveSpeed, agent.rushSpeed);
        }
    }
}