using UnityEngine;

public abstract class Enemy : Agent
{
    [Header("Common settings")] 
    public float battleTime;
    public bool isActive;
    
    protected float _defaultMoveSpeed;

    [SerializeField] protected LayerMask _whatIsPlayer;
    [SerializeField] protected LayerMask _whatIsObstacle;

    [Header("Attack Settings")] 
    public float runAwayDistance;
    public float attackDistance;
    public float attackCooldown;
    [SerializeField] protected int _maxCheckEnemy = 1;
    [HideInInspector] public float lastAttackTime;
    [HideInInspector] public Transform targetTrm;
    protected Collider2D[] _enemyCheckColliders;
    
    protected override void Awake()
    {
        base.Awake();
        _defaultMoveSpeed = Stat.moveSpeed;
        _enemyCheckColliders = new Collider2D[_maxCheckEnemy];
    }
    
    public virtual Collider2D IsPlayerDetected()
    {
        int cnt = Physics2D.OverlapCircleNonAlloc(transform.position, attackDistance, _enemyCheckColliders, _whatIsPlayer);
        
        return cnt >= 1 ? _enemyCheckColliders[0] : null;
    }
    
    public virtual bool IsObstacleDetected(float distance, Vector3 direction)
    {
        return Physics2D.Raycast(transform.position, direction, distance, _whatIsObstacle);
    }

    public abstract void AnimationEndTrigger();
    public override void SetDead()
    {
        isDead = true;
    }
}