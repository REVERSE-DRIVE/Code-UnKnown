using EnemyManage;

public class JailAttackState : EnemyAttackState
{
    private JailEnemy _jailEnemy => (JailEnemy)_enemyBase;
    public JailAttackState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _enemyBase.MovementCompo.StopImmediately();
        _jailEnemy.AttackArea.SetActive(true);
    }
}