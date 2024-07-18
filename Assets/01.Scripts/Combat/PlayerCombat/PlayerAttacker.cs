using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] private float distance = 5.0f;  // 레이의 거리
    public float angle = 45.0f;    // 부채꼴 각도
    public int numberOfRays = 10;  // 발사할 레이의 수
    public LayerMask layerMask;    // 충돌할 레이어 설정
    private Player _player;
    private Rigidbody2D _rigid; // movement랑 연동해야하나?
    [SerializeField] private float _detectDegree;
    [SerializeField] private int _rayAmount = 10;
    private Stat _attackRange;

    [Header("Current State")] 
    [SerializeField] private bool _isAttacking;
    [field:SerializeField] public int comboCount { get; private set; } = 0;
    private IDamageable _currentTarget;

    private Vector2 _direction;
    private void Awake()
    {
        _player.PlayerInputCompo.controlButtons.actionButton.onClick.AddListener(HandleAttack);
        _player.PlayerInputCompo.OnMovementEvent += HandleAiming;
    }

    private void Start()
    {
        _attackRange = _player.additionalStat.attackRange;
    }

    private void Update()
    {
        DetectEnemy();
    }

    public void Initialize(Player player)
    {
        _player = player;
        
    }

    public void HandleAttack()
    {
        // 공격 구현하자
        
    }

    public void HandleAiming(Vector2 direction)
    {
        _direction = direction;
    }

    void DetectEnemy()
    {
        Vector2 origin = transform.position;

        // 부채꼴 각도의 절반을 계산
        float halfAngle = angle / 2.0f;

        float angleStep = angle / (numberOfRays - 1);

        for (int i = 0; i < numberOfRays; i++)
        {
            // 각 레이의 각도를 계산
            float currentAngle = -halfAngle + (angleStep * i);
            Vector2 rayDirection = Quaternion.Euler(0, 0, currentAngle) * _direction;

            // 레이캐스트를 발사
            RaycastHit2D hit = Physics2D.Raycast(origin, rayDirection, distance, layerMask);
            Debug.DrawRay(origin, rayDirection * distance, Color.red);

            if (hit.collider != null)
            {
                // 충돌한 오브젝트가 있다면 여기서 처리
                Debug.Log("Hit: " + hit.collider.name);
            }
        }
    }
}
