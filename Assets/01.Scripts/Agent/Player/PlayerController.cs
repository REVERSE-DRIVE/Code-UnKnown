using System;
using UnityEngine;

public class PlayerController : AgentMovement
{
    private Player _player;
    private int _speedHash;

    private Vector2 _defaultScale = Vector2.one;
    private Vector2 _flipScale = new Vector2(-1,1);
    
    private void Start()
    {
        _player = _agent as Player;
        _speedHash = Animator.StringToHash("speed");
        _player.PlayerInputCompo.OnMovementEvent += SetMovement;
        _player.PlayerInputCompo.OnMovementEvent += HandleMovementRender;
    }

    private void HandleMovementRender(Vector2 direction)
    {
        if (direction.x > 0)
        {
            _visualTrm.localScale = _defaultScale;
        }else  if (direction.x < 0)
        {
            _visualTrm.localScale = _flipScale;
        }
        _player.AnimatorCompo.SetFloat(_speedHash, direction.magnitude);
    }
}