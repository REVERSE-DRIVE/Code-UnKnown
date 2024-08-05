using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class DefenderSkill : AttackCountSkill
{
    [SerializeField] private int _effectTime;
    [SerializeField] private int _buffPercent = 20;
    private int _buffValue;
    private Stat _defStat;

    protected override void Start()
    {
        base.Start();
        _defStat = player.Stat.defence;
        onAttackCountEvent += HandleDefenceUp;
    }

    public override bool UseSkill()
    {
        if (base.UseSkill()) return false;
        
        return true;
    }


    private void HandleDefenceUp()
    {
        StartCoroutine(DefenceUpCoroutine());
        // 방어력 증가랑 받는 피해 %감소는 다른 옵션이긴 함.
        // 다른 방법을 마련해야함
    }

    private IEnumerator DefenceUpCoroutine()
    {
        _buffValue = _defStat.GetPercent(_buffPercent);
        yield return new WaitForSeconds(_effectTime);
        
    }
}