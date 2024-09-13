using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AroundHoleObject : MonoBehaviour
{
    [SerializeField] float speed = 10;
    [SerializeField] float pushLength = 10;
    [SerializeField] Transform point;

    private void Update() {
        transform.RotateAround(point.position, new Vector3(0, 0, 1), Time.deltaTime * speed);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (!other.gameObject.TryGetComponent<JunkFileObject>(out var junSys)) return;
        
        junSys.EnableActive();
        
        Vector3 diff = other.transform.position - transform.position;
        other.rigidbody.AddForce(diff.normalized * pushLength, ForceMode2D.Impulse);
    }
}
