using System.Collections;
using System.Collections.Generic;
using EnemyManage;
using UnityEngine;

namespace EnemyManage {
    public class BossPillBodyCureWaveState : EnemyState<PillBodyStateEnum>
    {
        PillBody agent;
        float timer;

        public BossPillBodyCureWaveState(Enemy enemyBase, EnemyStateMachine<PillBodyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
        {
            agent = enemyBase as PillBody;
        }

        public override void Enter()
        {
            base.Enter();
            
            // 모여
            agent.EquipStatus.Start();

            timer = 0;
        }

        public override void UpdateState()
        {
            if (!agent.EquipStatus.IsSuccess()) return;

            timer += Time.deltaTime;
            if (timer < agent.cureWaveWait) return;
            
            var effect = GameObject.Instantiate(agent.cureWavePrefab).transform;
            effect.Find("Visual").localScale = new Vector3(agent.cureWaveRadius * 2, agent.cureWaveRadius * 2, 1);

            agent.DamageCasterCompo.CastDamage(agent.cureWaveRadius, agent.cureWaveDamage);
            CameraManager.Instance.Shake(10, 1);

            agent.LeftPiece.Stat.moveSpeed.AddModifier((int)(agent.LeftPiece.Stat.moveSpeed.GetValue() * agent.cureWaveSkillMoveUp));
            agent.RightPiece.Stat.moveSpeed.AddModifier((int)(agent.RightPiece.Stat.moveSpeed.GetValue() * agent.cureWaveSkillMoveUp));

            _stateMachine.ChangeState(PillBodyStateEnum.Idle);
        }

        public override void Exit()
        {
            base.Exit();
            agent.EquipStatus.Clear();
            agent.AllChangeState(PillPieceStateEnum.Disband);
        }
    }
}