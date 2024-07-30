using EnemyManage;

public abstract class EnemySkillState : EnemyState<EnemyStateEnum>
{
    public EnemySkillState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        _enemyBase.MovementCompo.StopImmediately();
        TakeSkill();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (_endTriggerCalled)
        {
            _stateMachine.ChangeState(EnemyStateEnum.Chase);
        }
    }

    protected abstract void TakeSkill();
}