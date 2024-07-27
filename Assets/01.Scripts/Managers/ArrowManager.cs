using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    [SerializeField] private List<EnemyDirectionArrow> _arrows;
    [SerializeField] private Transform _playerTrm;
    [SerializeField] private LayerMask _whatIsEnemy;
    [SerializeField] private float _detectRange;
    
    private Collider2D[] _colliders = new Collider2D[5];

    private void Start()
    {
        StartCoroutine(FindTargetCoroutine());
    }

    private IEnumerator FindTargetCoroutine()
    {
        while (true)
        {
            for (int i = 0; i < _arrows.Count; i++)
            {
                FindClosestEnemy(i);
            }

            yield return null;
        }
    }


    private void FindClosestEnemy(int index)
    {
        int hitCount = Physics2D.OverlapCircleNonAlloc(_playerTrm.position, _detectRange, _colliders, _whatIsEnemy);
        if (hitCount == 0) return;

        float minDistance = float.MaxValue;
        for (int i = 0; i < hitCount; i++)
        {
            float distance = Vector2.Distance(_playerTrm.position, _colliders[i].transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                if (!IsDuplicateTarget(_colliders[i].transform) && IsActiveTarget(_colliders[i].transform))
                    _arrows[index].Target = _colliders[i].transform;
            }
        }
    }

    private bool IsDuplicateTarget(Transform target)
    {
        for (int i = 0; i < _arrows.Count; i++)
        {
            if (_arrows[i].Target == target)
            {
                return true;
            }
        }

        return false;
    }
    
    private bool IsActiveTarget(Transform target)
    {
        return target.gameObject.activeSelf;
    }
}