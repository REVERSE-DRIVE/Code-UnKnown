using System.Collections;
using EnemyManage;
using UnityEngine;

public class ShoketyAttackState : EnemyAttackState
{
    private ShoketyEnemy _shoketyEnemy => _enemyBase as ShoketyEnemy;
    private RayDamageCaster _rayDamageCaster => _enemyBase.DamageCasterCompo as RayDamageCaster;
    private bool _isAttacking = false;
    private Coroutine _attackCoroutine;
    public ShoketyAttackState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        _shoketyEnemy.LineRendererCompo.enabled = true;
        _attackCoroutine = _shoketyEnemy.StartCoroutine(TakeDamage());
    }

    public override void UpdateState()
    {
        base.UpdateState();
        DrawLine();
    }

    private IEnumerator TakeDamage()
    {
        while (true)
        {
            if (!_shoketyEnemy.LineRendererCompo.enabled) continue;
            Vector2 direction = (_enemyBase.targetTrm.position - _enemyBase.transform.position).normalized;
            float distance = Vector2.Distance(_enemyBase.targetTrm.position, _enemyBase.transform.position);
            _rayDamageCaster.Init(_enemyBase, direction);
            _rayDamageCaster._distance = distance;
            _rayDamageCaster.CastDamage(_enemyBase.Stat.GetDamage());
            yield return new WaitForSeconds(2f);
        }
    }

    public override void Exit()
    {
        _shoketyEnemy.StopCoroutine(_attackCoroutine);
        base.Exit();
    }

    private void DrawLine()
    {
        _shoketyEnemy.LineRendererCompo.SetPosition(0, _enemyBase.transform.localPosition);
        _shoketyEnemy.LineRendererCompo.SetPosition(1, _enemyBase.targetTrm.transform.localPosition);
    }

    protected override void AttackEnd()
    {
        _shoketyEnemy.LineRendererCompo.enabled = false;
        base.AttackEnd();
    }
}