using System.Collections;
using EnemyManage;
using ObjectPooling;
using UnityEngine;

public class UndefinedAttackState : EnemyAttackState
{
    // Bullet Setting
    private int _damage;
    private float _speed = 10f;
    private float _lifeTime = 3f;
    
    public UndefinedAttackState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        _damage = _enemyBase.Stat.GetDamage();
    }
    
    public override void Enter()
    {
        base.Enter();
        _enemyBase.StartCoroutine(Shot());
    }

    private IEnumerator Shot()
    {
        for (int i = 0; i < 3; i++)
        {
            var bullet = PoolingManager.Instance.Pop(PoolingType.Projectile_Normal) as Projectile;
            bullet.Initialize
                (_enemyBase.transform.position, _damage, _speed, _lifeTime);
            Vector3 dir = (_enemyBase.targetTrm.position - _enemyBase.transform.position).normalized;
            bullet.Shoot(dir);
            yield return new WaitForSeconds(0.05f);
        }
    }
}