using EnemyManage;
using UnityEngine;

public class MeltingEnemy : EnemyBase
{
    [field: SerializeField] public AreaAttack AttackAreaPrefab { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        StateMachine.AddState(EnemyStateEnum.Idle, new EnemyIdleState(this, StateMachine, "Idle"));
        StateMachine.AddState(EnemyStateEnum.Chase, new TargetFollowChaseState(this, StateMachine, "Chase", 3f));
        StateMachine.AddState(EnemyStateEnum.Attack, new MeltingAttackState(this, StateMachine, "Attack"));
        StateMachine.AddState(EnemyStateEnum.Dead, new EnemyDeadState(this, StateMachine, "Dead"));
    }
}