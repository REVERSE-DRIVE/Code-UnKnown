using EnemyManage;

// 플레이어를 발견하면 3초간 사라짐. 3초 뒤 플레이어의 뒤에 나타나서 플레이어를 휘둘러 공격함. 
// 나타난 후, 3초간의 정적 상태가 되며, 정적 상태가 끝나면 똑간은 공격을 행한다. (공격 당 1회 공격)
public class UndefinedEnemy : EnemyBase
{
    protected override void Awake()
    {
        base.Awake();
        StateMachine.AddState(EnemyStateEnum.Idle, new EnemyIdleState(this, StateMachine, "Idle"));
        StateMachine.AddState(EnemyStateEnum.Chase, new EnemyChaseState(this, StateMachine, "Chase"));
        StateMachine.AddState(EnemyStateEnum.Attack, new UndefinedAttackState(this, StateMachine, "Attack"));
        StateMachine.AddState(EnemyStateEnum.Dead, new EnemyDeadState(this, StateMachine, "Dead"));
    }
}