using System.Collections;
using EnemyManage;
using UnityEngine;

public class ShoketySkillState : EnemySkillState
{
    private OverlapDamageCaster _overlapDamageCaster => (_enemyBase as ShoketyEnemy).OverlapDamageCaster;
    private bool _isTakeSkill = false;
    public ShoketySkillState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    protected override void TakeSkill()
    {
        if (_isTakeSkill)
        {
            return;
        }
        _overlapDamageCaster.CastDamage(_enemyBase.Stat.GetDamage(), 2f);
        _isTakeSkill = true;
        _enemyBase.StartCoroutine(StopPlayer());
        
    }

    private IEnumerator StopPlayer()
    {
        Player player =_enemyBase.targetTrm.GetComponent<Player>();
        player.MovementCompo.StopImmediately();
        player.MovementCompo.isStun = true;
        yield return new WaitForSeconds(3f);
        player.MovementCompo.isStun = false;
        _isTakeSkill = false;
    }
}