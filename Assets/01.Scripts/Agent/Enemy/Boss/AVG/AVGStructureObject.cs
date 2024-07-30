using ObjectManage;
using UnityEngine;
using UnityEngine.Serialization;

public class AVGStructureObject : DestroyableObject
{
    [SerializeField] private GameObject _shieldObject;
    [SerializeField] private SpriteRenderer _thisObjectVisual;
    [SerializeField] private ParticleSystem _destroyParticle;
    private Transform _bossTrm;
    [SerializeField] private float _shieldRadius = 1.5f;
    [Header("State Information")] 
    [SerializeField] private bool _isShieldActivated;

    private Collider2D _collider;
    private Player _player;
    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _thisObjectVisual = transform.Find("Visual").GetComponent<SpriteRenderer>();
    }

   

    public void Active(Transform bossTrm)
    { // 레드 스테이트가 활성화됨
        
        _bossTrm = bossTrm;
        _player = PlayerManager.Instance.player;
        transform.position =  (Vector2)_bossTrm.position + (Random.insideUnitCircle * 9);
        SetDefault();
    }

    public void SetDefault()
    {
        _shieldObject.SetActive(false);

        _isShieldActivated = false;
        _thisObjectVisual.enabled = true;
        _collider.enabled = true;
    }

    /**
     * <param name="hitDamage">
     * 만약 실드가 없이 직격으로 피격시 들어올 AVG보스의 버스트 대미지
     * </param>
     * <summary>
     *
     * </summary>
     */
    public void DefenseAVGBurst(int hitDamage)
    {
        bool isSafeZone = 
            Vector2.Distance(
                PlayerManager.Instance.player.transform.position, 
                transform.position) < _shieldRadius;
        if (isSafeZone && _isShieldActivated) return;
        _player.HealthCompo.TakeDamage(hitDamage);
        
    }
    
    


    protected void DestroyEvent()
    {
        _shieldObject.SetActive(true);
        _destroyParticle.Play();
    }

    public void Destroy()
    {
        DestroyEvent();
        _thisObjectVisual.enabled = false;
        _isShieldActivated = true;
    }

    public void OffObject()
    {
        _shieldObject.SetActive(false);
        _thisObjectVisual.enabled = false;
        _collider.enabled = false;
    }

    
}