using System;
using ObjectManage;
using UnityEngine;

public class PlayerInteractController : MonoBehaviour
{
    [SerializeField] private LayerMask _objectLayeer;
    [SerializeField] private float _interactRange;
    
    private Player _player;

    private InteractObject _currentInteractObject;
    
    private void FixedUpdate()
    {
        DetectObjects();
    }

    private void DetectObjects()
    {
        Collider2D hit =  Physics2D.OverlapCircle(transform.position, _interactRange, _objectLayeer);
        
        if (hit == null)
        {
            if (_currentInteractObject == null)
                return;
            // _currentInteractObject 에서 나갈때 이벤트 실행
        }
        // 감지되었을때 이벤트 실행
    }

}