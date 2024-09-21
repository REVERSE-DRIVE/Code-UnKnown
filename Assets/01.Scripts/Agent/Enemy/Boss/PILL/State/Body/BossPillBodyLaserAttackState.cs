using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using EnemyManage;
using UnityEngine;

namespace EnemyManage {
    public class BossPillBodyLaserAttackState : EnemyState<PillBodyStateEnum>
    {
        PillBody agent;
        IEnumerator processHnadle = null;
        Sequence sequence;
        
        public BossPillBodyLaserAttackState(Enemy enemyBase, EnemyStateMachine<PillBodyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
        {
            agent = enemyBase as PillBody;
        }

        public override void Enter()
        {
            base.Enter();
            agent.EquipStatus.Start();
            processHnadle = null;
            sequence = null;
        }

        public override void UpdateState()
        {
            if (!agent.EquipStatus.IsSuccess() || processHnadle != null) return;
            processHnadle = StartLaser();
            agent.StartCoroutine(processHnadle);
        }

        public override void Exit()
        {
            base.Exit();
            agent.EquipStatus.Clear();
            agent.AllChangeState(PillPieceStateEnum.Disband);

            agent.transform.eulerAngles = Vector3.zero; // 돌리는거 원상복귀
            
            if (processHnadle != null) {
                agent.StopCoroutine(processHnadle);
                processHnadle = null;
            }

            if (sequence != null) {
                sequence.Kill();
                sequence = null;
            }
        }

        IEnumerator StartLaser() {
            yield return new WaitForSeconds(0.5f);
            agent.AllChangeState(PillPieceStateEnum.LeaserAttack);

            CameraManager.Instance.Shake(agent.laserCamShakePower, agent.laserRotateDuration);
            
            float timer = 0;
            while ((timer += Time.deltaTime) < agent.laserRotateDuration) {
                yield return null;
                agent.transform.Rotate(new Vector3(0,0,agent.laserRotateSpeed) * Time.deltaTime, Space.World);
            }

            // 그만
            agent.AllChangeState(PillPieceStateEnum.Idle);

            sequence = DOTween.Sequence();
            sequence.Append(agent.transform.DORotate(Vector3.zero, 0.5f));
            sequence.OnComplete(() => _stateMachine.ChangeState(PillBodyStateEnum.Idle));
        }
    }
}