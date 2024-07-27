using UnityEngine;
using UnityEngine.Tilemaps;

public class ClearZone : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _damageCooltime;
    private float _currentTime = 0;
    private bool _isCoolDowned;
    
    [SerializeField] [ColorUsage(true, true)] private Color _defaultColor;
    private float _defaultRange = 0.1f;
    //[SerializeField] [ColorUsage(true, true)] private Color _defaultLineColor;
    [Space(10f)]
    [SerializeField] [ColorUsage(true, true)] private Color _detectedColor;
    private float _detectedRange = 5f;

    private TilemapRenderer _tilemapRenderer;
    private Material _material;
    private int _colorHash;
    private int _rangeHash;
    private int _emissionColorHash;

    private void Awake()
    {
        _tilemapRenderer = GetComponent<TilemapRenderer>();
        _material = _tilemapRenderer.material;
        _colorHash = Shader.PropertyToID("_Color");
        _rangeHash = Shader.PropertyToID("_Range");
        _emissionColorHash = Shader.PropertyToID("_EmissionColor");
    }

    private void Update()
    {
        if (_isCoolDowned) return;

        _currentTime += Time.deltaTime;
        if (_currentTime >= _damageCooltime)
        {
            _isCoolDowned = true;
            SetZoneActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!_isCoolDowned) return;
        if (other.CompareTag("Player"))
        {
            IDamageable player = other.transform.GetComponent<IDamageable>();
            CameraManager.Instance.Shake(10,0.3f);
            player.TakeDamage(_damage);
            _isCoolDowned = false;
            _currentTime = 0;
            // 나중에 감염도 알림을 띄워야한다.
            SetZoneActive(false);
        }
    }

    private void SetZoneActive(bool value)
    {
        if (value)
        {
            _material.SetColor(_colorHash, _defaultColor);
            _material.SetColor(_emissionColorHash, _defaultColor);
            _material.SetFloat(_rangeHash, _defaultRange);
        }
        else
        {
            _material.SetColor(_colorHash, _detectedColor);
            _material.SetColor(_emissionColorHash, _detectedColor);
            _material.SetFloat(_rangeHash, _detectedRange);
        }
    }
}
