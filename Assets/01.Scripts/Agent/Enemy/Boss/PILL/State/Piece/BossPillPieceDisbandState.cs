using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace EnemyManage {
    public class BossPillPieceDisbandState : EnemyState<PillPieceStateEnum>
    {
        public BossPillPieceDisbandState(Enemy enemyBase, EnemyStateMachine<PillPieceStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
        {
        }

        Sequence _sequence;

        public override void Enter()
        {
            base.Enter();

            // 루트 변경
            _enemyBase.transform.SetParent(null, true);

            _sequence = DOTween.Sequence();

            // 조금뒤로 빼기
            Vector3 movePos = _enemyBase.transform.position;
            movePos += _enemyBase.transform.up * 0.2f;

            _sequence.Append(_enemyBase.transform.DOMove(movePos, 0.3f).SetEase(Ease.Linear));
            _sequence.AppendInterval(0.3f);

            // 방향 원래로
            _sequence.Append(_enemyBase.transform.DORotate(Vector3.zero, 0.3f));

            _sequence.OnComplete(() => _stateMachine.ChangeState(PillPieceStateEnum.TargetFollow));
        }

        public override void Exit()
        {
            base.Exit();

            _sequence?.Kill();
            _sequence = null;
        }
    }
}
