using System;

public class PlayerController : AgentMovement
{
    private Player _player;
    private void Start()
    {
        _player = _agent as Player;
        _player.PlayerInputCompo.OnMovementEvent += SetMovement;
    }
}