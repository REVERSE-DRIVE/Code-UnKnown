using System.Collections;
using UnityEngine;

public class EnemyLaser : Projectile
{
    [SerializeField] private LayerMask _whatIsPlayer;
    public void Shot(Vector3 position)
    {
        StartCoroutine(Move(position, 0.05f));
    }

    private IEnumerator Move(Vector3 position, float f)
    {
        float currentTime = 0;
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = position;
        while (currentTime < f)
        {
            currentTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, targetPosition, currentTime / f);
            Vector3 dir = (targetPosition - startPosition).normalized;
            ShotRay(dir);
            yield return null;
        }
        PoolingManager.Instance.Push(this);
    }

    private void ShotRay(Vector3 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 100, _whatIsPlayer);
        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent(out Player player))
            {
                player.HealthCompo.TakeDamage(_damage);
            }
        }
    }
}
