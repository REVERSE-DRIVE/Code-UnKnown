using UnityEngine;

public class KeepInScreenUI : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _offset = 0.1f;
    
    private void Update()
    {
        Vector3 pos = _camera.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp(pos.x, _offset, 1 - _offset);
        pos.y = Mathf.Clamp(pos.y, _offset, 1 - _offset);
        transform.position = _camera.ViewportToWorldPoint(pos);
    }
}