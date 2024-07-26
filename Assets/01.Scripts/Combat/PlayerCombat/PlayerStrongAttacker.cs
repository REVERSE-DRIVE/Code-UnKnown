using System;
using System.Collections;
using UnityEngine;

public class PlayerStrongAttacker : MonoBehaviour
{
    public Action OnHoldAttackEvent;

    private Player _player;
    private PlayerComboCounter _comboCounter;

    private int _damageBuffValue;
    
    private void Awake()
    {
        _player = GetComponent<Player>();
        _comboCounter = GetComponent<PlayerComboCounter>();
    }

    private void Start()
    {
        _player.PlayerInputCompo.controlButtons.actionButton.OnHoldEvent += HandleStrongAttack;
    }

    public void HandleStrongAttack()
    {
        print("홀드 공격 시작");

        int combo = _comboCounter.comboCount;
        if (combo < 5)
            return;

        if (combo < 10)
        {
            UseShield();
        }else if (combo < 20)
        {
            UseIncAtk();
        }else if (combo < 30)
        {
            
        }else
        {
            
        }
        _comboCounter.ResetCombo();
        //_comboCounter
    }

    private void UseShield()
    {
        
    }

    private void UseIncAtk()
    {
        StartCoroutine(IncAtkCoroutine());
    }

    private IEnumerator IncAtkCoroutine()
    {
        _damageBuffValue = (int)(_player.Stat.GetDamage() * (5 + _comboCounter.comboCount)/100f);
        _player.Stat.damage.AddModifier(_damageBuffValue);
        yield return new WaitForSeconds(5f);
        _player.Stat.damage.RemoveModifier(_damageBuffValue);
    }
    
}