using System;
using System.Collections;
using UnityEngine;

public class LoadingInfinityScroll : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _offset = 0f;
    public bool IsScrolling { get; set; } = true;

    private void Start()
    {
        StartCoroutine(Scroll());
    }

    private IEnumerator Scroll()
    {
        while (true)
        {
            if (!IsScrolling)
            {
                _speed += 0.1f;
            }
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
            if (transform.position.y >= _offset)
            {
                transform.position = new Vector3(transform.position.x, -_offset, transform.position.z);
            }
            yield return null;
        }
    }
}