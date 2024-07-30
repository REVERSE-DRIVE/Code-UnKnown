using System.Collections;
using ObjectManage;
using UnityEngine;

public class JunkFileObject : InteractObject, IDamageable
{
    private Rigidbody2D _rigidCompo;
    [SerializeField] protected int _currentHealth;

    [SerializeField] private Material _hitMaterial;
    [SerializeField] private bool _collisionDestroy;
    [SerializeField] private float _pushPower;
    private Vector2 _origin = Vector2.zero;
    [SerializeField] private int _damage = 3;
    private bool _isActive;
    private WaitForSeconds ws = new WaitForSeconds(0.05f);
    [SerializeField] private ParticleSystem _pushVFX;
    [SerializeField] private Transform _arrowTrm;
    private Collider2D _collider;
    private int _dissolveHash;
    private Agent _agent;
    private Vector2 _direction;
    
    private void Awake()
    {
        _rigidCompo = GetComponent<Rigidbody2D>();
        // 나중에 _agent에 기본으로 플레이어를 할당해주어야함
        OnInteractEvent += HandlePush;
        _collider = GetComponent<Collider2D>();
        _dissolveHash = Shader.PropertyToID("_DissolveLevel");
        
        OnDetectedEvent += HandleDetected;
        OnUnDetectedEvent += HandleUnDetected;
    }

    protected override void Start()
    {
        base.Start();
        _agent = PlayerManager.Instance.player;
    }

    private void Update()
    {
        if (!_isActive)
        {
            _rigidCompo.velocity = Vector2.zero;
            if (isDetected)
            {
                Vector2 direction = transform.position - _agent.transform.position;
                _arrowTrm.right = direction;
            }
            return;

        }
            
        float magnitude = _rigidCompo.velocity.magnitude;
        if (magnitude <= 0.1f)
        {
            _isActive = false;
            
        }
    }

    private void HandleDetected()
    {
        _arrowTrm.gameObject.SetActive(true);
    }
    private void HandleUnDetected()
    {
        _arrowTrm.gameObject.SetActive(false);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!_isActive) return;
        if(other.transform.TryGetComponent(out Health health))
            HandleCollisionEvent(health);
        
        if (!_collisionDestroy) return;

        _currentHealth--;
        if (_currentHealth <= 0)
        {
            Destroy();
        }
    }

    public override void Interact(InteractData data)
    {
        if (_isActive) return;
        _origin = data.interactOriginPosition;
        base.Interact(data);
        _isActive = true;
        _pushVFX.Play();
    }

    private void HandlePush()
    {
        _direction = ((Vector2)transform.position - _origin).normalized;
        //_rigidCompo.AddForce(direction.normalized * _pushPower, ForceMode2D.Impulse);
        _rigidCompo.velocity = _direction * _pushPower;
        StartCoroutine(PushCoroutine());
    }

    private void HandleCollisionEvent(Health hit)
    {
        hit.TakeDamage(_damage);
        CameraManager.Instance.Shake(5, 0.1f);
    }

    private IEnumerator PushCoroutine()
    {
        for (int i = 0; i < 3; i++)
        {
            // _visualRenderer.material = _hitMaterial;
            // yield return ws;
            // _visualRenderer.material = _defaultMaterial;
            // yield return ws;

            _visualRenderer.color = Color.red;
            yield return ws;
            _visualRenderer.color = Color.white;
            yield return ws;

        }
        
    }

   
    
    private void Destroy()
    {
        _collider.enabled = false;
        _visualRenderer.material = _defaultMaterial;
        _rigidCompo.velocity = Vector2.zero;
        
        StartCoroutine(DestroyCoroutine());
    }

    private IEnumerator DestroyCoroutine()
    {
        float currentTime = 0;
        float dissolveTime = 1.2f;
        while (currentTime < dissolveTime)
        {
            currentTime += Time.deltaTime;
            _defaultMaterial.SetFloat(_dissolveHash, Mathf.Lerp(1f, 0f, currentTime / dissolveTime));
            yield return null;
            
        }
    }
    
    public void TakeDamage(int amount)
    {
        if (_isActive) return;
        _currentHealth -= amount;
        Player player = PlayerManager.Instance.player;
        player.additionalStat.isStrongAttack = true;
        Interact(new InteractData{interactOriginPosition = player.transform.position, interactOwner = player});
        CheckDie();
        
    }

    public void RestoreHealth(int amount)
    {
        _currentHealth += amount;
        CheckDie();
    }

    public void CheckDie()
    {
        if (_currentHealth <= 0)
        {
            OnDestroyEvent?.Invoke();
        }
    }
    
    
}