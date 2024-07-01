using System.Collections;
using EnemyManage;
using UnityEngine;

public class MeleeChaseState : EnemyState<MeleeStateEnum>
{
    private EnemyMovement _movementCompo;
    private Coroutine _chaseCoroutine;
    private bool _isChase;
    public MeleeChaseState(Enemy enemyBase, EnemyStateMachine<MeleeStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        _movementCompo = _enemyBase.MovementCompo as EnemyMovement;
    }
    
    public override void Enter()
    {
        base.Enter();
        _chaseCoroutine = _enemyBase.StartCoroutine(Chase());
    }

    private IEnumerator Chase()
    {
        WaitForSeconds ws = new WaitForSeconds(1f);
        if (_isChase) yield break;
        _isChase = true;
        while (_isChase)
        {
            Vector3 dir = _movementCompo.GetRandomPosition();
            _enemyBase.MovementCompo.SetMovement(dir);
            yield return ws;
            _enemyBase.MovementCompo.StopImmediately();
            yield return ws;
        }
    }

    public override void UpdateState()
    {
        base.UpdateState();
        Debug.Log(_chaseCoroutine == null);
        float distance = (_enemyBase.targetTrm.position - _enemyBase.transform.position).magnitude;
        
        if (distance <= _enemyBase.attackDistance)
        {
            _enemyBase.StopAllCoroutines();
            _isChase = false;
            _stateMachine.ChangeState(MeleeStateEnum.Attack);
        }
        else
        {
            _chaseCoroutine = _enemyBase.StartCoroutine(Chase());
        }
    }
}