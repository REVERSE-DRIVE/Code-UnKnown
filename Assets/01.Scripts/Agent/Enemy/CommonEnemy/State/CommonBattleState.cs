using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonBattleState : EnemyState<CommonStateEnum>
{
    protected readonly int _velocityHash = Animator.StringToHash("Velocity");
    private EnemyMovement movementCompo;
    private Coroutine _randomMoveCoroutine;
    bool isChasing = false;
    
    public CommonBattleState(Enemy enemyBase, EnemyStateMachine<CommonStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        movementCompo = _enemyBase.MovementCompo as EnemyMovement;
    }

    private Vector3 _targetDestination; //목적지

    public override void UpdateState()
    {
        base.UpdateState();
        
        float distance = (_targetDestination - _enemyBase.transform.position).magnitude;
        if (distance > 0.5f)
        {
            isChasing = false;
            SetDestination(_enemyBase.targetTrm.position);
        }
        
        
        
        float targetDistance = (_enemyBase.targetTrm.position - _enemyBase.transform.position).magnitude;
        
        bool playerInRange = targetDistance <= _enemyBase.attackDistance;
        bool cooldownPass = _enemyBase.lastAttackTime + _enemyBase.attackCooldown <= Time.time;
        if (playerInRange && cooldownPass)
        {
            _stateMachine.ChangeState(CommonStateEnum.Attack);
        }
        else if (playerInRange)
        {
            movementCompo.LookToTarget(_enemyBase.targetTrm.position);
            movementCompo.StopImmediately();
        }
        else
        {
            if (_randomMoveCoroutine == null)
            {
                _randomMoveCoroutine = _enemyBase.StartCoroutine(RandomMove());
            }
        }
        
        float velocity = _enemyBase.MovementCompo.Velocity.magnitude;
        _enemyBase.AnimatorCompo.SetFloat(_velocityHash, velocity);
    }

    private IEnumerator RandomMove()
    {
        if (isChasing)
        {
            yield break;
        }
        isChasing = true;
        while (true)
        {
            Vector3 randomPos = _enemyBase.GetRandomPosition();
            SetDestination(randomPos);
            yield return new WaitForSeconds(0.5f);
            movementCompo.StopImmediately();
            yield return new WaitForSeconds(1f);
        }
    }

    public override void Enter()
    {
        base.Enter();
        SetDestination(_enemyBase.targetTrm.position);
    }
    
    // 목적지 설정
    private void SetDestination(Vector3 position)
    {
        _targetDestination = position;
        _enemyBase.MovementCompo.SetMovement(position);
    }
}