using UnityEngine;

namespace EnemyManage {
    public class BossPillPieceTargetRushState : EnemyState<PillPieceStateEnum>
    {
        PillPiece agent;

        public BossPillPieceTargetRushState(Enemy enemyBase, EnemyStateMachine<PillPieceStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
        {
            agent = enemyBase as PillPiece;
        }
        
        Vector3 rushDir;
        float timer;

        public override void Enter()
        {
            base.Enter();

            timer = 0;

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
            
            agent.MovementCompo.SetMovement(rushDir);
        }

        public override void Exit()
        {
            base.Exit();
            agent.Stat.RemoveModifier(StatType.MoveSpeed, agent.rushSpeed);
        }
    }
}