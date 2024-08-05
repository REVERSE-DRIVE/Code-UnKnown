public abstract class AttackTriggerSkill : Skill
{
    protected int _attackCount;
    protected override void Start()
    {
        base.Start();
        player.PlayerAttackCompo.OnAttackEvent += HandlePlayerAttackEvent;
    }

    protected virtual void HandlePlayerAttackEvent()
    {
        _attackCount++;
    }
}