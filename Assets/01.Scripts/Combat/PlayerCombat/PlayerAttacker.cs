using System.Collections;
using ObjectManage;
using ObjectPooling;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    private Player _player;
    private IMovement _movementCompo;
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

    [Header("Combo Setting")] 
    [SerializeField] private float _comboCancelTime;
    private float _comboTime = 0;
    public bool IsCombo => _comboTime < _comboCancelTime;
    [field:SerializeField] public int comboCount { get; private set; } = 0;

    
    [Header("Current State")] 
    [SerializeField] private bool _isTargeting;
    [SerializeField] private bool _isAttacking;
    private IDamageable _currentTarget;
    private Transform _currentTargetTrm;

    private Vector2 _direction;
    private Vector2 _origin;
    private float _currentTime = 0;
    
    private void Awake()
    {
        _player = GetComponent<Player>();
        _movementCompo = GetComponent<IMovement>();
    }

    private void Start()
    {
         _player.PlayerInputCompo.controlButtons.actionButton.OnTapEvent += HandleAttack;
         _player.PlayerInputCompo.OnMovementEvent += HandleAiming;
        _attackRange = _player.additionalStat.attackRange;
        _comboRate = _player.additionalStat.comboBonusRate;
    }

    private void Update()
    {
        DetectEnemy();
        if (_isTargeting)
        {
            _attackEffect.SetTarget(_currentTargetTrm.position);
        }
        _currentTime += Time.deltaTime;
        _comboTime += Time.deltaTime;
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

            if(hit.transform.TryGetComponent(out IDamageable target))
            {
                isNoTarget = false;
                _currentTargetTrm = hit.transform;
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
        _attackEffect.RefreshLine(_currentTargetTrm.position);

    }
    
    private void HandleTargetEmpty()
    {
        _attackEffect.SetLineActive(false);
        _attackEffect.SetRangeActive(true);
        _attackEffect.SetRangeSize(_attackRange.GetValue());
        _attackEffect.SetTargetActive(false);
        _isTargeting = false;
    }
    
    public void HandleAttack()
    {
        if (_currentTime < 0.1f) return;
        if (_isTargeting && !_isAttacking)
        {
            _isAttacking = true;
            Vector2 attackDirection = _currentTargetTrm.position - transform.position;
            StartCoroutine(AttackCoroutine(attackDirection));
        }
    }

    private IEnumerator AttackCoroutine(Vector2 boundDir)
    {
        Combo();
        
        _player.Stat.isResist = true;
        float duration = Mathf.Clamp01(0.5f - _player.additionalStat.dashSpeed.GetValue() * 0.1f) * boundDir.magnitude / 15;
        _attackEffect.SetTargetAttack(true);

        yield return _player.PlayerController.Dash(_currentTargetTrm.position, duration);
        _attackEffect.Play(boundDir.normalized);
        EffectObject effect = PoolingManager.Instance.Pop(_hitVFX) as EffectObject;
        effect.Initialize(_currentTargetTrm.position);
        _currentTarget.TakeDamage(CalcDamage());
        yield return new WaitForSeconds(0.2f);
        _attackEffect.SetTrailActive(true);
        _attackEffect.SetTargetAttack(false);
        _movementCompo.GetKnockBack(_direction.normalized * _boundPower, 0.2f);
        yield return new WaitForSeconds(0.2f);
        _currentTime = 0;
        _attackEffect.SetTrailActive(false);
        _isAttacking = false;
        _player.Stat.isResist = false;
        
        
    }

    private int CalcDamage()
    {
        int damage = _player.Stat.GetDamage();
        if (Random.Range(0, 10) < _comboRate.GetValue())
            damage += comboCount;

        return damage;
    }
    
    private void DashCoroutine()
    {
        
    }

    private void Combo()
    {
        if (IsCombo)
        {
            comboCount++;
            TextEffectObject textEffect = PoolingManager.Instance.Pop(PoolingType.TextEffectObject) as TextEffectObject;
            textEffect.Initialize(new TextContent
            {
                color = Color.Lerp(Color.white, Color.red, comboCount / 10f),
                content = $"{comboCount}<color=red>HIT</color>",
                size = 11,
                lifeTime = 0.7f
            }, ((Vector2)_currentTargetTrm.position + Random.insideUnitCircle * 2));
            textEffect.Play();
        }
        else
            comboCount = 0;    
        _comboTime = 0f;
    }

}
