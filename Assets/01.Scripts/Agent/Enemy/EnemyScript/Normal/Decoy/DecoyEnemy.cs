// 플레이어를 향해 다가오며, 공격 거리가 될 시, 칼을 휘둘러 피해를 입힌다. 

using EnemyManage;

public class DecoyEnemy : EnemyBase
{
    protected override void Awake()
    {
        base.Awake();
        StateMachine.AddState(EnemyStateEnum.Idle, new EnemyIdleState(this, StateMachine, "Idle"));
        StateMachine.AddState(EnemyStateEnum.Chase, new DecoyChaseState(this, StateMachine, "Chase"));
        StateMachine.AddState(EnemyStateEnum.Attack, new EnemyAttackState(this, StateMachine, "Attack"));
        StateMachine.AddState(EnemyStateEnum.Dead, new DecoyDeadState(this, StateMachine, "Dead"));
    }
}