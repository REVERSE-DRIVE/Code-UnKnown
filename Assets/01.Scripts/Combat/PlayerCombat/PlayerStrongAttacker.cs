using System;
using UnityEngine;

public class PlayerStrongAttacker : MonoBehaviour
{
    public Action OnHoldAttackEvent;

    private Player _player;
    private PlayerComboCounter _comboCounter;

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
        //_comboCounter
    }
    
    
}