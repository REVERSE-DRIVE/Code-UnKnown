// 플레이어를 향해 다가오며, 공격 거리가 될 시, 칼을 휘둘러 피해를 입힌다. 

using EnemyManage;
using UnityEngine;

public class DecoyEnemy : EnemyBase
{
    [field:SerializeField] public DecoySword SwordCompo { get; private set; } 
    protected override void Awake()
    {
        base.Awake();
        StateMachine.AddState(EnemyStateEnum.Idle, new EnemyIdleState(this, StateMachine, "Idle"));
        StateMachine.AddState(EnemyStateEnum.Chase, new DecoyChaseState(this, StateMachine, "Chase", 0));
        StateMachine.AddState(EnemyStateEnum.Attack, new DecoyAttackState(this, StateMachine, "Attack"));
        StateMachine.AddState(EnemyStateEnum.Dead, new DecoyDeadState(this, StateMachine, "Dead"));
    }

    protected override void Update()
    {
        base.Update();
        Debug.Log("A<Color=red>"+StateMachine.CurrentState+"</Color>");
    }
}