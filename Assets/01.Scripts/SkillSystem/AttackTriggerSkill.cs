public abstract class AttackTriggerSkill : Skill
{
    protected int _attackCount;
    protected override void Start()
    {
        base.Start();
        player.PlayerAttackCompo.OnAttackEvent += HandleAttack;
    }

    private void HandleAttack()
    {
        if (!skillEnabled) return;

        HandlePlayerAttackEvent();
    }

    protected virtual void HandlePlayerAttackEvent()
    {
        _attackCount++;
    }
}