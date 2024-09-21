using System.Collections;
using System.Collections.Generic;
using EnemyManage;
using UnityEngine;

namespace EnemyManage {
    public class BossPillBodyShockWaveState : EnemyState<PillBodyStateEnum>
    {
        PillBody agent;
        float timer;

        public BossPillBodyShockWaveState(Enemy enemyBase, EnemyStateMachine<PillBodyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
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
            if (timer < agent.shockWaveWait) return;
            
            var effect = GameObject.Instantiate(agent.shockWavePrefab, agent.transform.position, Quaternion.identity).transform;
            effect.Find("Visual").localScale = new Vector3(agent.shockWaveRadius * 2, agent.shockWaveRadius * 2, 1);

            agent.DamageCasterCompo.CastDamage(agent.shockWaveRadius, agent.shockWaveDamage);
            CameraManager.Instance.Shake(10, 1);

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