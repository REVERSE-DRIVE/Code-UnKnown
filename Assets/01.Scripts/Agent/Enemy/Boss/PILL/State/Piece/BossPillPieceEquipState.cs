using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace EnemyManage {
    public class BossPillPieceEquipState : EnemyState<PillPieceStateEnum>
    {
        enum ProcessStatus {
            Follow, // 따라가는중...
            Rotate, // 돌리는쥬ㅜㅇ;..
            Wait,
            End
        }

        PillEquipStatus status;
        PillPiece agent;
        Transform point;

        Sequence sequence;
        ProcessStatus process;
        

        public BossPillPieceEquipState(Enemy enemyBase, EnemyStateMachine<PillPieceStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
        {
            agent = enemyBase as PillPiece;
        }

        public override void Enter()
        {
            base.Enter();
            agent.ColliderCompo.enabled = false;
            
            if (status == null) {
                status = agent.Body.EquipStatus;
            }

            point = agent.Body.GetEquipPoint(agent.Direction);
            process = ProcessStatus.Follow;
            sequence = null;
        }

        public override void Exit()
        {
            base.Exit();
            sequence?.Kill();
            agent.ColliderCompo.enabled = true;
        }

        public override void UpdateState()
        {
            switch (process)
            {
                case ProcessStatus.Follow:
                    FollowPoint();
                    break;
                case ProcessStatus.Rotate:
                    MyRotate();
                    break;
                case ProcessStatus.Wait:
                    WaitLast();
                    break;
            }
        }

        void FollowPoint() {
            Vector3 pointPos = point.position + (point.up * agent.disbandBack);

            if (Vector3.Distance(pointPos, agent.transform.position) < 0.1f) {
                process = ProcessStatus.Rotate;
                agent.transform.position = pointPos;
                agent.MovementCompo.StopImmediately();

                // 부모 바꿈
                agent.transform.SetParent(agent.Body.transform, true);
                return;
            }

            Vector3 diff = pointPos - agent.transform.position;
            agent.MovementCompo.SetMovement(diff);

            // 내눈을 바라봐
            float rotationZ = Mathf.Acos(diff.x / diff.magnitude) * 180 / Mathf.PI * Mathf.Sign(diff.y);
            agent.transform.rotation = Quaternion.Lerp(agent.transform.rotation, Quaternion.Euler(0, 0, rotationZ + 90), Time.deltaTime * agent.lookSpeed);
        }

        void MyRotate() {
            if (sequence != null) return;

            sequence = DOTween.Sequence();
            sequence.Append(agent.transform.DORotateQuaternion(point.rotation, 0.3f));
            sequence.OnComplete(() => {
                process = ProcessStatus.Wait;
                agent.Body.FinishEquip(agent.Direction);
            });
        }

        void WaitLast() {
            if (!agent.Body.EquipStatus.IsSuccess()) return;

            process = ProcessStatus.End;
            sequence = DOTween.Sequence();
            sequence.Append(agent.transform.DOMove(point.position, agent.disbandDuration));
            sequence.OnComplete(() => _stateMachine.ChangeState(PillPieceStateEnum.Idle));
        }
    }
}
