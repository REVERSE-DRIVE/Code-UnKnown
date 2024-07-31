using EnemyManage;

public class InducedEleteEnemy : EnemyBase
{
    protected override void Awake()
    {
        base.Awake();
        StateMachine.AddState(EnemyStateEnum.Idle, new EnemyIdleState(this, StateMachine, "Idle"));
        StateMachine.AddState(EnemyStateEnum.Chase, new EnemyChaseState(this, StateMachine, "Chase"));
        StateMachine.AddState(EnemyStateEnum.Attack, new InducedEleteAttackState(this, StateMachine, "Attack"));
        StateMachine.AddState(EnemyStateEnum.Skill, new InducedEleteSkillState(this, StateMachine, "Skill"));
        StateMachine.AddState(EnemyStateEnum.Dead, new EnemyDeadState(this, StateMachine, "Dead"));
    }
}