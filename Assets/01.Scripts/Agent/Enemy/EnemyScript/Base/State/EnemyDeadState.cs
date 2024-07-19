using EnemyManage;
using UnityEngine;

public class EnemyDeadState : EnemyState<EnemyStateEnum>
{
    public EnemyDeadState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        _enemyBase.ColliderCompo.enabled = false;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        _enemyBase.MovementCompo.StopImmediately();
        if (_endTriggerCalled)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy Dead");
        PoolingManager.Instance.Push(_enemyBase as EnemyBase);
        _enemyBase.isDead = true;
    }
}