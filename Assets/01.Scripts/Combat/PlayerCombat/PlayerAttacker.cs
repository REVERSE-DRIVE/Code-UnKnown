using System;
using System.Collections;
using ObjectManage;
using ObjectPooling;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAttacker : MonoBehaviour
{
    public event Action OnAttackEvent;
    
    [Header("Attack Setting")]
    [SerializeField] private float _detectDistance = 5.0f;  // 레이의 거리
    [SerializeField] private float _detectDegree = 70f;
    [SerializeField] private int _rayAmount = 10;
    [SerializeField] private LayerMask layerMask;    // 충돌할 레이어 설정
    [SerializeField] private float _boundPower = 30f;
    private Stat _attackRange;
    private Stat _comboRate;
    [SerializeField] private PlayerAttackEffect _attackEffect;
    [SerializeField] private PoolingType _hitVFX;
    
    [Header("Current State")] 
    [SerializeField] private bool _isTargeting;
    [SerializeField] private bool _isAttacking;

    private CameraManager _cameraManager;
    private Player _player;
    private PlayerComboCounter _comboCounter;
    private IMovement _movementCompo;
    private IDamageable _currentTarget;
    public Transform currentTargetTrm;
    private Vector2 _direction;
    private Vector2 _origin;
    private float _currentTime = 0;
    private int immediateDef;
    
    private void Awake()
    {
        _player = GetComponent<Player>();
        _movementCompo = GetComponent<IMovement>();
        _comboCounter = GetComponent<PlayerComboCounter>();
    }

    private void Start()
    {
         _player.PlayerInputCompo.controlButtons.actionButton.OnTapEvent += HandleAttack;
         _player.PlayerInputCompo.OnMovementEvent += HandleAiming;
        _attackRange = _player.additionalStat.attackRange;
        _comboRate = _player.additionalStat.comboBonusRate;
        _cameraManager = CameraManager.Instance;
    }

    private void Update()
    {
        DetectEnemy();
        if (_isTargeting)
        {
            _attackEffect.SetTarget(currentTargetTrm.position);
            _attackEffect.SetStrongAttackMode(_player.additionalStat.isStrongAttack);
        }
        _currentTime += Time.deltaTime;
    }

    public void Initialize(Player player)
    {
        _player = player;
        
    }

   
    public void HandleAiming(Vector2 direction)
    {
        if (direction.magnitude < 0.1f) return;
        _direction = direction;
    }
    
    public void HandleAttack()
    {
        if (_currentTime < 0.1f) return;
        if (_isTargeting && !_isAttacking)
        {
            _isAttacking = true;
            Vector2 attackDirection = currentTargetTrm.position - transform.position;
            StartCoroutine(AttackCoroutine(attackDirection));
        }
    }
    private void DetectEnemy()
    {
        _origin = transform.position;
        // 부채꼴 각도의 절반을 계산
        float halfAngle = _detectDegree / 2.0f;
        float angleStep = _detectDegree / (_rayAmount - 1);
        bool isNoTarget = true;
        for (int i = 0; i < _rayAmount; i++)
        {
            // 각 레이의 각도를 계산
            float currentAngle = -halfAngle + (angleStep * i);
            Vector2 rayDirection = (Quaternion.Euler(0, 0, currentAngle) * _direction).normalized;

            // 레이캐스트를 발사
            RaycastHit2D hit = Physics2D.Raycast(_origin, rayDirection, _attackRange.GetValue(), layerMask);
            Debug.DrawRay(_origin, rayDirection * _attackRange.GetValue(), Color.red);
            
            if (hit.collider == null)
            {
                continue;
            }

            Transform targetTrm = hit.collider.transform;
            if(targetTrm.TryGetComponent(out IDamageable target))
            {
                isNoTarget = false;
                currentTargetTrm = targetTrm;
                _currentTarget = target;
                HandleTargeted();
                break;
            }
        }

        if (isNoTarget)
        {
            HandleTargetEmpty();
        }       
    }

    private void HandleTargeted()
    {
        _isTargeting = true;
        _attackEffect.SetLineActive(true);
        _attackEffect.SetRangeActive(false);
        _attackEffect.RefreshLine(currentTargetTrm.position);

    }
    
    private void HandleTargetEmpty()
    {
        _attackEffect.SetLineActive(false);
        _attackEffect.SetRangeActive(true);
        _attackEffect.SetRangeSize(_attackRange.GetValue());
        _attackEffect.SetTargetActive(false);
        _isTargeting = false;
    }
    
    

    private IEnumerator AttackCoroutine(Vector2 boundDir)
    {
        HandleAttackJudge();
        immediateDef = _player.Stat.defence.GetValue() * 5;
        _player.Stat.defence.AddModifier(immediateDef);
        //_player.Stat.isResist = true;
        float duration = Mathf.Clamp01(1.5f - _player.additionalStat.dashSpeed.GetValue() * 0.3f) * boundDir.magnitude / 15;
        _attackEffect.SetTargetAttack(true);
        _cameraManager.ZoomFromDefault(8f,0.2f);
        yield return _player.PlayerController.Dash(currentTargetTrm.position, duration);
        _attackEffect.Play(boundDir.normalized);
        EffectObject effect = PoolingManager.Instance.Pop(_hitVFX) as EffectObject;
        effect.Initialize(currentTargetTrm.position);
        ApplyDamage();
        yield return new WaitForSeconds(0.2f);
        _attackEffect.SetTrailActive(true);
        _attackEffect.SetTargetAttack(false);
        _movementCompo.GetKnockBack(_direction.normalized * _boundPower, 0.2f);
        _cameraManager.ZoomDefault(0.15f);
        yield return new WaitForSeconds(0.2f);
        _currentTime = 0;
        _attackEffect.SetTrailActive(false);
        _isAttacking = false;
        _player.Stat.defence.RemoveModifier(immediateDef);
        
    }
    public void HandleAttackJudge()
    {
        OnAttackEvent?.Invoke();
        CountCombo();
    }

    private void ApplyDamage()
    {
        if (currentTargetTrm.TryGetComponent(out IStrongDamageable strongHit) && _player.additionalStat.isStrongAttack)
        {
            strongHit.TakeStrongDamage(10);
        }
        int damage = CalcDamage();
        
        _currentTarget.TakeDamage(damage);

    }

    private int CalcDamage()
    {
        int damage = _player.Stat.GetDamage();
        if (Random.Range(0, 10) < _comboRate.GetValue())
            damage += _comboCounter.comboCount;
        if (_player.additionalStat.isStrongAttack)
        {
            damage = (int)(damage * 1.5f);
            _player.additionalStat.isStrongAttack = false;
        }
        return damage;
    }

    private void CountCombo()
    {
        _comboCounter.CountCombo();
        TextEffectObject textEffect = PoolingManager.Instance.Pop(PoolingType.TextEffectObject) as TextEffectObject;
        textEffect.Initialize(new TextContent
        {
            color = Color.Lerp(Color.white, Color.red, _comboCounter.comboCount / 10f),
            content = $"{_comboCounter.comboCount}<color=red>HIT</color>",
            size = 11,
            lifeTime = 0.7f
        }, ((Vector2)currentTargetTrm.position + Random.insideUnitCircle * 2));
        textEffect.Play();
    }

    

   

}
