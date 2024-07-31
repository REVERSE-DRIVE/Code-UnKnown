using DG.Tweening;
using PlayerPartsManage;
using UnityEngine;

public class CounterBodyPart : PlayerPart
{
    [SerializeField] private LayerMask _whatIsEnemy;
    [SerializeField] private ParticleSystem _explosion;
    [SerializeField] private Transform _shockWave;
    private int count = 0;
    private Collider2D[] _colliders;
    public CounterBodyPart(Player owner) : base(owner)
    {
    }

    protected override void Awake()
    {
        base.Awake();
        _colliders = new Collider2D[10];
    }

    public override void OnMount()
    {
        _owner.HealthCompo.OnHealthChangedValueEvent += Shock;
    }

    public override void OnUnMount()
    {
        _owner.HealthCompo.OnHealthChangedValueEvent -= Shock;
    }

    private void Shock(int prevvalue, int newvalue, int max)
    {
        if (prevvalue > newvalue)
        {
            count++;
        }
        if (count >= 3)
        {
            Instantiate(_explosion, _owner.transform.position, Quaternion.identity);
            _shockWave.DOPunchScale(new Vector3(2f, 2f, 1), 0.5f, 1).OnComplete(() =>
            {
                _shockWave.transform.localScale = Vector3.zero;
            });
            CameraManager.Instance.Shake(5, 0.5f);
            int enemyCount = 
                Physics2D.OverlapBoxNonAlloc(_owner.transform.position, new Vector2(3, 3), 0, _colliders, _whatIsEnemy);
            for (int i = 0; i < enemyCount; i++)
            {
                if (_colliders[i].TryGetComponent(out Health enemy))
                {
                    enemy.TakeDamage(50);
                }
            }
            count = 0;
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(_owner.transform.position, new Vector3(3, 3, 0));
    }
}