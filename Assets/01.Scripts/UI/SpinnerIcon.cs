using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerIcon : MonoBehaviour
{
    [SerializeField] AnimationCurve curve;
    [SerializeField] float speed = 1f;
    
    private void Update() {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, curve.Evaluate((Time.time * speed) % 1.0f)) * -360);
    }
}
