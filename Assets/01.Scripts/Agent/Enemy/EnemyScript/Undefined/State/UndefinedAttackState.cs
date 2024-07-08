using DG.Tweening;
using EnemyManage;

public class UndefinedAttackState : EnemyAttackState
{
    public UndefinedAttackState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        _enemyBase.MovementCompo.StopImmediately();
        _enemyBase.RendererCompo.DOFade(1, 0.5f);
    }
    
}