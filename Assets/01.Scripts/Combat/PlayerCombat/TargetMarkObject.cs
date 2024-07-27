using System;
using UnityEngine;

public class TargetMarkObject : MonoBehaviour
{
    [SerializeField] private Sprite[] _targetMarkSprites;
    private SpriteRenderer _spriteRenderer;


    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetAttack(bool value)
    {
        if (value)
        {
            transform.rotation = Quaternion.Euler(0,0,45f);
            transform.localScale = Vector2.one * 2;
        }
        else
        {
            transform.rotation = Quaternion.identity;
            transform.localScale = Vector2.one * 2.3f;
        }
    }

    public void SetStrongAttackMode(bool value)
    {
        _spriteRenderer.sprite = _targetMarkSprites[value ? 1 : 0];
    }
}