using EnemyManage;
using ObjectManage;
using ObjectPooling;
using UnityEngine;

public class DecoyDeadState : EnemyDeadState
{
    public DecoyDeadState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    protected override void Die()
    {
        base.Die();
        _enemyBase.DamageCasterCompo.CastDamage();
        var effect = PoolingManager.Instance.Pop(PoolingType.DecoyExplodeVFX) as ParticleVFXObject;
        effect.transform.position = _enemyBase.transform.position;
        effect.Play();
        CameraManager.Instance.Shake(10f, 0.1f);
    }
}