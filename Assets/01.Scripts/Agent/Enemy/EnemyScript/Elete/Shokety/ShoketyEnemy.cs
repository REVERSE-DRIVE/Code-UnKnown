using EnemyManage;
using UnityEngine;

public class ShoketyEnemy : EnemyBase
{
    [field:SerializeField] public LineRenderer LineRendererCompo { get; private set; }
    [field:SerializeField] public OverlapDamageCaster OverlapDamageCaster { get; private set; }
    
    protected override void Awake()
    {
        base.Awake();
        StateMachine.AddState(EnemyStateEnum.Idle, new EnemyIdleState(this, StateMachine, "Idle"));
        StateMachine.AddState(EnemyStateEnum.Chase, new TargetFollowChaseState(this, StateMachine, "Chase", 3f));
        StateMachine.AddState(EnemyStateEnum.Attack, new ShoketyAttackState(this, StateMachine, "Attack"));
        StateMachine.AddState(EnemyStateEnum.Skill, new ShoketySkillState(this, StateMachine, "Skill"));
        StateMachine.AddState(EnemyStateEnum.Dead, new EnemyDeadState(this, StateMachine, "Dead"));
    }
}