using System;
using System.Collections;
using UnityEngine;

public class EnemyLaser : Projectile
{
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
            yield return null;
        }
    }
}
