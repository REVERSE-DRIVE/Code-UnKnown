using DG.Tweening;
using ObjectPooling;
using UnityEngine;

namespace ObjectManage
{
    
    public class TurretObject : DestroyableObject, IPoolable
    {
        [SerializeField] private Sprite[] _turretSprites;
        [SerializeField] private Vector2[] _directions;
        [SerializeField] private int _rayAmount = 9;
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private Transform _firePos;
        [SerializeField] private PoolingType _destroyVFX;
        [SerializeField] private ParticleSystem _fireVFX;
        [Header("Turret Detect Setting")]
        [SerializeField] private float _detectDistance;
        [SerializeField] private float _detectOffMinDistance; // 이 거리내의 목표는 타게팅하지 않음
        [SerializeField] private float _detectDegree;
        [SerializeField] private bool _isSetAutoRandomDirection;
        [Space(10f)]
        [Header("Turret Fire Setting")]
        [SerializeField] private float _angleErrorRange;
        [SerializeField] private PoolingType _projectile;
        [SerializeField] private float _attackCooltime;
        [Header("Projectile Setting")]
        [SerializeField] private int _damage;
        [SerializeField] private float _speed;
        [SerializeField] private float _lifeTime = 10f;

        private Transform _visualTrm;
        private Transform _headTrm;
        private SpriteRenderer _headSpriteRenderer;
        private int _randomIndex;
        private Vector2 _origin;
        private Vector2 _direction;
        private Vector2 _targetPosition;
        private Transform _targetTrm;
        private bool _isTargeted;
        private float _attackTime;
        private float _currentTime = 0;
        
        public PoolingType type { get; set; }
        public GameObject ObjectPrefab => gameObject;
        
        public void ResetItem()
        {
            if (_isSetAutoRandomDirection)
            {
                _randomIndex = Random.Range(0, _turretSprites.Length);
                _direction = _directions[_randomIndex];
                _headSpriteRenderer.sprite = _turretSprites[_randomIndex];
            }
        }

        private void Awake()
        {
            _visualTrm = transform.Find("Visual");
            _headTrm = _visualTrm.Find("Head");
            _headSpriteRenderer = _headTrm.GetComponent<SpriteRenderer>();
            OnDestroyEvent += HandleDestroy;
            ResetItem();
        }

        private void Update()
        {
            DetectTarget();
            Fire();
        }

        private void DetectTarget()
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
                RaycastHit2D hit = Physics2D.Raycast(_origin, rayDirection, _detectDistance, _targetLayer);
                Debug.DrawRay(_origin, rayDirection * _detectDistance, Color.red);
            
                if (hit.collider == null)
                {
                    continue;
                }

                Transform targetTrm = hit.collider.transform;
                if (Vector2.Distance(targetTrm.position, transform.position) < _detectOffMinDistance) return;
                
                if(targetTrm.TryGetComponent(out IDamageable target))
                {
                    isNoTarget = false;
                    if (_isTargeted == false)
                        _attackTime += 5f;
                    _isTargeted = true;
                    _targetTrm = targetTrm;
                    _targetPosition = _targetTrm.position;
                    _attackTime += Time.deltaTime;
                    HandleTargeted();
                    break;
                }
            }

            if (isNoTarget)
            {
                _isTargeted = false;
            }
        }

        private void HandleTargeted()
        {
            
        }

        private void Fire()
        {
            if (_attackTime <= 0) return;

            _attackTime -= Time.deltaTime;
            _currentTime += Time.deltaTime;

            if (_currentTime >= _attackCooltime)
            {
                _currentTime = 0f;
                Vector2 newDirection = (_targetPosition +
                                       Vector2.one * Random.Range(-_angleErrorRange, _angleErrorRange))
                    - (Vector2)transform.position;
                Projectile projectile = PoolingManager.Instance.Pop(_projectile) as Projectile;
                projectile.Initialize((Vector2)_firePos.position + _direction, _damage, _speed, _lifeTime);
                projectile.Shoot(newDirection.normalized);
                _fireVFX.Play();
                _headTrm.DOShakePosition(0.09f, 0.3f, 20);

            }
        }

        private void HandleDestroy()
        {
            EffectObject effect = PoolingManager.Instance.Pop(_destroyVFX) as EffectObject;
            EffectObject explode = PoolingManager.Instance.Pop(PoolingType.ExplodeMark) as EffectObject;
            explode.Initialize(transform.position);
            effect.Initialize(transform.position);
            effect.Play();
            Destroy(gameObject); // 나중에 풀링으로 교체
            
        }
    }

}