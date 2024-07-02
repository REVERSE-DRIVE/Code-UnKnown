using System.Collections;
using EnemyManage;
using UnityEngine;

public class MeleeChaseState : EnemyState<MeleeStateEnum>
{
    public MeleeChaseState(Enemy enemyBase, EnemyStateMachine<MeleeStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }
}