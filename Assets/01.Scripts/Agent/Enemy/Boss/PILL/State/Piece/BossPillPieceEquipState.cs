using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace EnemyManage {
    public class BossPillPieceEquipState : EnemyState<PillPieceStateEnum>
    {
        public BossPillPieceEquipState(Enemy enemyBase, EnemyStateMachine<PillPieceStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
        {
        }
    }
}
