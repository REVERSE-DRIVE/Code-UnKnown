using EnemyManage;

// 정의되지 않은 반투명한 탄환을 플레이어를 향해 일직선으로 발사하며, 이동속도가 굉장히 빠르다.  
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