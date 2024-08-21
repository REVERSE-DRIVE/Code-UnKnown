using EnemyManage;

// 플레이어를 향해 레이저 발사 레이저 피격 시 3초간 지속 데미지 부여
public class TypeAUpgradeEnemy : EnemyBase
{
    protected override void Awake()
    {
        base.Awake();
        StateMachine.AddState(EnemyStateEnum.Idle, new EnemyIdleState(this, StateMachine, "Idle"));
        StateMachine.AddState(EnemyStateEnum.Chase, new EnemyChaseState(this, StateMachine, "Chase", 1f));
        StateMachine.AddState(EnemyStateEnum.Attack, new TypeAUpgradeAttackState(this, StateMachine, "Attack"));
        StateMachine.AddState(EnemyStateEnum.Dead, new EnemyDeadState(this, StateMachine, "Dead"));   
    }
}