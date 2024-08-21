using System.Collections;
using EnemyManage;
using UnityEngine;

public class EnemyChaseState : EnemyState<EnemyStateEnum>
{
    protected EnemyMovement _movementCompo;
    protected bool _isChase = false;
    protected EnemyBase _enemy;
    protected float _chaseTime = 0.5f;
    private Coroutine _chaseCoroutine;
    
    public EnemyChaseState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName, float chaseTime) : base(enemyBase, stateMachine, animBoolName)
    {
        _movementCompo = _enemyBase.MovementCompo as EnemyMovement;
        _enemy = enemyBase as EnemyBase;
        this._chaseTime = chaseTime;
    }

    public override void Enter()
    {
        base.Enter();
        _chaseCoroutine = _enemyBase.StartCoroutine(ChaseRoutine());
        if (_chaseCoroutine == null) Debug.Log("ChaseCoroutine is null");
    }

    public override void UpdateState()
    {
        base.UpdateState();
        _movementCompo.LookToTarget(_enemyBase.targetTrm.position);
        float distance = (_enemyBase.targetTrm.position - _enemyBase.transform.position).magnitude;
        
        if (distance <= _enemyBase.chaseDistance && distance > _enemyBase.attackDistance)
        {
            if (_chaseCoroutine != null)
            {
                _enemyBase.StopCoroutine(_chaseCoroutine);
            }
            _isChase = false;
            _enemyBase.MovementCompo.StopImmediately();
            _enemyBase.MovementCompo.SetMovement(_enemyBase.targetTrm.position - _enemyBase.transform.position);
            
        }
        else if (distance <= _enemyBase.attackDistance)
        {
            if (_chaseCoroutine != null)
            {
                _enemyBase.StopCoroutine(_chaseCoroutine);
            }
            _isChase = false;
            if (_enemy.IsElete)
            {
                int random = Random.Range(0, 2);
                Debug.Log(random);
                if (random == 0)
                {
                    _stateMachine.ChangeState(EnemyStateEnum.Attack);
                }
                else
                {
                    _stateMachine.ChangeState(EnemyStateEnum.Skill);
                }
                return;
            }
            _stateMachine.ChangeState(EnemyStateEnum.Attack);
        }
        else
        {
            _chaseCoroutine = _enemyBase.StartCoroutine(ChaseRoutine());
        }
    }
    
    public override void Exit()
    {
        if (_chaseCoroutine != null)
            _enemyBase.StopCoroutine(_chaseCoroutine);
        base.Exit();
    }
    
    protected virtual IEnumerator ChaseRoutine()
    {
        if (_isChase) yield break;
        _isChase = true;
        while (true)
        {
            Vector3 randomDir = _movementCompo.GetRandomPosition();
            _enemyBase.MovementCompo.SetMovement(randomDir);
            _movementCompo.LookToTarget(randomDir);
            yield return new WaitForSeconds(_chaseTime);
            _enemyBase.MovementCompo.StopImmediately();
            yield return new WaitForSeconds(_chaseTime);
        }
    }
    
    
}