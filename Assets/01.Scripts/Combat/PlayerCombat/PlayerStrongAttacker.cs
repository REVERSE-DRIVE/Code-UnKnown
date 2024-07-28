using System;
using System.Collections;
using UnityEngine;

public class PlayerStrongAttacker : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayer;

    [Header("Range Blading Setting")]
    [SerializeField] private int _rangeAttackAmount = 7;
    [SerializeField] private float _rangeAttackSize = 8f;
    [SerializeField] private PlayerHoldEffect _effect;
    public Action OnHoldAttackEvent;

    [Header("Dash Attack Setting")]
    [SerializeField] private float _dashWidth;
    [SerializeField] private int _dashNormalDamage;
    [SerializeField] private int _dashDamagePercent;
    [SerializeField] private float _dashDistance = 10f;
    private Player _player;
    private PlayerComboCounter _comboCounter;

    private int _damageBuffValue;
    private Collider2D[] _hits;
    private RaycastHit2D[] _castHits;
    
    private void Awake()
    {
        _player = GetComponent<Player>();
        _comboCounter = GetComponent<PlayerComboCounter>();
    }

    private void Start()
    {
        _player.PlayerInputCompo.controlButtons.actionButton.OnHoldEvent += HandleStrongAttack;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _comboCounter.BonusCombo(5);
        }
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
            RangeAttack();
        }else
        {
            DashAttack();
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

    #region Range Blading

    private void RangeAttack()
    {
        StartCoroutine(RangeAttackCoroutine());
    }

    private IEnumerator RangeAttackCoroutine()
    {
        yield return StartCoroutine(_effect.RangeSizeUp(_rangeAttackSize, 1f));
        CameraManager.Instance.Shake(8f, 2f);
        _effect.PlayBlading(1f);
        StartCoroutine(RangeTargetAttackCoroutine());

    }

    private IEnumerator RangeTargetAttackCoroutine()
    {
        for (int i = 0; i < _rangeAttackAmount; i++)
        {
            AttackRangeTarget();
            yield return new WaitForSeconds(0.2f);
        }
        // 적 공격 구현
        yield return null;
    }

    private void AttackRangeTarget()
    {
        int damage = _player.Stat.GetDamage();
        _hits = new Collider2D[10];
        int amount = Physics2D.OverlapCircleNonAlloc(transform.position, _rangeAttackSize, _hits, _targetLayer);
        if (amount == 0) return;
        for (int i = 0; i < amount; i++)
        {
            if (_hits[i].transform.TryGetComponent(out IDamageable hit))
            {
                hit.TakeDamage(damage);
                _player.PlayerAttackCompo.HandleAttackJudge();
            }
        }
    }

    #endregion

    private void DashAttack()
    {
        StartCoroutine(DashCoroutine());
    }

    private IEnumerator DashCoroutine()
    {
        Time.timeScale = 0.5f;
        Vector2 attackDirection = _player.PlayerInputCompo.Direction;
        VolumeEffectManager.Instance.SetGrayScale(-80f, 0.3f,  0.2f);
        _effect.PlayDashReady();
        yield return new WaitForSeconds(0.2f);
        CameraManager.Instance.Shake(9f, 0.1f);
        _effect.PlayDashImpact(attackDirection);
        Dash(attackDirection);
        Time.timeScale = 1f;
    }

    private void Dash(Vector2 direction)
    {
        _player.MovementCompo.StopImmediately();
        _castHits = new RaycastHit2D[10];
        int amount = Physics2D.CircleCastNonAlloc(transform.position, _dashWidth, direction, _castHits, _dashDistance, _targetLayer);
        if (amount == 0) return;
        for (int i = 0; i < amount; i++)
        {
            if (_castHits[i].transform.TryGetComponent(out IDamageable hit))
            {
                hit.TakeDamage(_dashNormalDamage);
                Health health = hit as Health;
                if (health != null)
                {
                    int percentDamage = (int)(health.maxHealth * _dashDamagePercent / 100f);
                    health.TakeDamage(percentDamage);
                }
                _player.PlayerAttackCompo.HandleAttackJudge();
            }
        }

    }
    
}