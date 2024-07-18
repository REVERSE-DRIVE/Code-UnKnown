using System.Collections;
using ObjectManage;
using ObjectPooling;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    private Player _player;
    private IMovement _movementCompo;
    [SerializeField] private float _detectDistance = 5.0f;  // 레이의 거리
    [SerializeField] private float _detectDegree = 70f;
    [SerializeField] private int _rayAmount = 10;
    [SerializeField] private LayerMask layerMask;    // 충돌할 레이어 설정
    [SerializeField] private float _boundPower = 30f;
    private Stat _attackRange;
    [SerializeField] private PlayerAttackEffect _attackEffect;
    [SerializeField] private PoolingType _hitVFX;

    [Header("Current State")] 
    [SerializeField] private bool _isTargeting;
    [SerializeField] private bool _isAttacking;
    [field:SerializeField] public int comboCount { get; private set; } = 0;
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
         _player.PlayerInputCompo.controlButtons.actionButton.onClick.AddListener(HandleAttack);
         _player.PlayerInputCompo.OnMovementEvent += HandleAiming;
        _attackRange = _player.additionalStat.attackRange;
    }

    private void Update()
    {
        DetectEnemy();
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
            RaycastHit2D hit = Physics2D.Raycast(_origin, rayDirection, _detectDistance, layerMask);
            Debug.DrawRay(_origin, rayDirection * _detectDistance, Color.red);
            
            if (hit.collider == null)
            {
                continue;
            }

            if (_isTargeting) continue;
            if(hit.transform.TryGetComponent(out IDamageable target))
            {
                _isTargeting = true;
                _currentTargetTrm = hit.transform;
                _currentTarget = target;
                return;
            }
        }

        if (isNoTarget)
        {
            _isTargeting = false;
        }        
    }
    
    public void HandleAttack()
    {
        // 공격 구현하자

        if (_currentTime < 0.1f) return;
        if (_isTargeting && !_isAttacking)
        {
            
            _isAttacking = true;
            _currentTime = 0;
            Vector2 attackDirection = _currentTargetTrm.position - transform.position;
            _attackEffect.Play(attackDirection.normalized);
            EffectObject effect = PoolingManager.Instance.Pop(_hitVFX) as EffectObject;
            effect.Initialize(_currentTargetTrm.position);
            transform.position = _currentTargetTrm.position;
            _currentTarget.TakeDamage(_player.Stat.GetDamage());
            StartCoroutine(AttackCoroutine(attackDirection));
            
        }
    }

    private IEnumerator AttackCoroutine(Vector2 boundDir)
    {
        yield return new WaitForSeconds(0.1f);
        _movementCompo.GetKnockBack(_direction.normalized * _boundPower, 0.23f);
        _isAttacking = false;
    }

}
