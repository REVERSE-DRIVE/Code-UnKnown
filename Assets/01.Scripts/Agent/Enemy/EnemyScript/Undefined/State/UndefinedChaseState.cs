using System.Collections;
using DG.Tweening;
using EnemyManage;
using ObjectPooling;
using UnityEngine;

public class UndefinedChaseState : EnemyChaseState
{
    public UndefinedChaseState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        _enemyBase.MovementCompo.StopImmediately();
        var projectile =
            PoolingManager.Instance.Pop(PoolingType.Projectile_Tracing) as TracingProjectile;
        projectile.Shoot(_enemyBase.targetTrm.position);
    }
}