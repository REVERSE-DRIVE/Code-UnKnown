using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDirectionArrow : MonoBehaviour
{
    [SerializeField] private RectTransform _arrowTrm;
    [SerializeField] private Transform _targetTrm;
    [SerializeField] private LayerMask _whatIsEnemy;

    private Collider2D[] _colliders;
    private bool isFindTarget;
    private Image _arrowImage;

    private void Awake()
    {
        _arrowImage = GetComponent<Image>();
    }

    private void Start()
    {
        _colliders = new Collider2D[1];
    }

    private void Update()
    {
        Debug.Log("Update");
        LookToEnemy();
        FindClosestEnemy();
        TargetInScreen();
        _arrowImage.enabled = !isFindTarget;
    }
    
    private void TargetInScreen()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(_targetTrm.position);
        if (screenPos.x < 0 || screenPos.x > Screen.width || screenPos.y < 0 || screenPos.y > Screen.height)
        {
            isFindTarget = false;
        }
    }
    
    

    private void FindClosestEnemy()
    {
        if (isFindTarget) return;
        int hits = Physics2D.OverlapCircleNonAlloc(transform.position, 10f, _colliders, _whatIsEnemy);
        if (hits > 0)
        {
            isFindTarget = true;
            _targetTrm = _colliders[0].transform;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 10f);
    }

    private void LookToEnemy()
    {
        Vector3 dir = _targetTrm.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        _arrowTrm.rotation = Quaternion.Euler(0, 0, angle);
    }
}