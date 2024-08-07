using PlayerPartsManage;
using UnityEngine;

public class InfectBodyPart : PlayerPart
{
    [SerializeField] private LayerMask _whatIsEnemy;
    [SerializeField] private Collider2D[] _hits;
    public InfectBodyPart(Player owner) : base(owner)
    {
    }
    
    public override void OnMount()
    {
        _owner.PlayerAttackCompo.OnAttackEvent += Attack;
    }

    public override void OnUnMount()
    {
        _owner.PlayerAttackCompo.OnAttackEvent -= Attack;
    }

    private void Attack()
    {
        int random = Random.Range(0, 100);
        if (random < 100)
        {
            _hits = new Collider2D[10];
            int Overlap = Physics2D.OverlapCircleNonAlloc(_owner.transform.position, 3f, _hits, _whatIsEnemy);
            for (int i = 0; i < Overlap; i++)
            {
                if (_hits[i].TryGetComponent(out EnemyBase enemy))
                {
                    enemy.OnFaint(3f);
                }
            }
        }
    }
}