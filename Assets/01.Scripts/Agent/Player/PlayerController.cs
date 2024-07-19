using System;
using System.Collections;
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

    public Coroutine Dash(Vector2 targetPosition, float duration)
    {
        return StartCoroutine(DashCoroutine(targetPosition, duration));
    }

    private IEnumerator DashCoroutine(Vector2 targetPos, float duration)
    {
        if (duration == 0)
        {
            transform.position = targetPos;
            yield break;
        }
        float currentTime = 0;
        Vector2 beforePos = transform.position;
        float ratio = 0;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            ratio = currentTime / duration;
            transform.position = Vector2.Lerp(beforePos, targetPos, ratio);
            yield return null;
        }
        transform.position = targetPos;
    }
}