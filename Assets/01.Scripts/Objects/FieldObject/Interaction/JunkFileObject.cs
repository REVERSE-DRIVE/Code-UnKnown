using System;
using System.Collections;
using ObjectManage;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class JunkFileObject : InteractObject
{
    private Rigidbody2D _rigidCompo;
    [SerializeField] private Material _hitMaterial;
    [SerializeField] private bool _collisionDestroy;
    [SerializeField] private float _pushPower;
    private Vector2 _origin = Vector2.zero;
    [SerializeField] private int _damage = 3;
    private bool _isActive;
    private WaitForSeconds ws = new WaitForSeconds(0.05f);
    [SerializeField] private ParticleSystem _pushVFX;

    private Collider2D _collider;
    private int _dissolveHash;
    
    private void Awake()
    {
        _rigidCompo = GetComponent<Rigidbody2D>();

        OnInteractEvent += HandlePush;
        _collider = GetComponent<Collider2D>();
        _dissolveHash = Shader.PropertyToID("_DissolveLevel");
    }

    private void Update()
    {
        if (!_isActive)
        {
            _rigidCompo.velocity = Vector2.zero;
            return;

        }
            
        float magnitude = _rigidCompo.velocity.magnitude;
        if (magnitude <= 0.1f)
        {
            _isActive = false;
            
        }
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
        _origin = data.interactOriginPosition;
        base.Interact(data);
        _isActive = true;
        _pushVFX.Play();
        print("정크 파일 상호작용됨");
    }

    private void HandlePush()
    {
        Vector2 direction = (Vector2)transform.position - _origin;
        //_rigidCompo.AddForce(direction.normalized * _pushPower, ForceMode2D.Impulse);
        _rigidCompo.velocity = direction.normalized * _pushPower;
        StartCoroutine(PushCoroutine());
    }

    private void HandleCollisionEvent(Health hit)
    {
        hit.TakeDamage(_damage);
        
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
    
    
}