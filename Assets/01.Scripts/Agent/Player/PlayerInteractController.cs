using System;
using ObjectManage;
using UnityEngine;

public class PlayerInteractController : MonoBehaviour
{
    [SerializeField] private LayerMask _objectLayeer;
    [SerializeField] private float _interactRange;
    
    private Player _player;

    private InteractObject _currentInteractObject;
    private Collider2D _hitTarget;
    private void FixedUpdate()
    {
        DetectObjects();
    }

    private void DetectObjects()
    {
        _hitTarget = Physics2D.OverlapCircle(transform.position, _interactRange, _objectLayeer);
        
        if (_hitTarget == null)
        {
            if (_currentInteractObject == null)
                return;
            _currentInteractObject.UnDetected();
            _currentInteractObject = null;
            // _currentInteractObject 에서 나갈때 이벤트 실행
            return;
        }
        
        // 감지되었을때 이벤트 실행
        if (_hitTarget.TryGetComponent(out InteractObject interactTarget))
        {
            interactTarget.Detected();
        }    
    }

}