using EnemyManage;
using UnityEngine;

public class MeltingAttackState : EnemyAttackState
{
    private MeltingEnemy _meltingEnemy;
    public MeltingAttackState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        _meltingEnemy = enemyBase as MeltingEnemy;
    }

    public override void Enter()
    {
        base.Enter();
        AreaAttack area = GameObject.Instantiate(_meltingEnemy.AttackAreaPrefab, _meltingEnemy.transform.position, Quaternion.identity);
        area.GetDamage(3, 3);
        
    }
}