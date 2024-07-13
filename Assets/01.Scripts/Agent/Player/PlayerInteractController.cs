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

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void Start()
    {
        _player.PlayerInputCompo.controlButtons.OnInteractEvent += Interact;

    }

    private void FixedUpdate()
    {
        DetectObjects();
    }

    public void Interact()
    {

        if (_currentInteractObject.isDetected)
        {
            _currentInteractObject.Interact(
                new InteractData()
                {
                    
                    interactOwner = _player,
                    interactOriginPosition = transform.position
                }
                );
        }
    }

    private void DetectObjects()
    {
        _hitTarget = null;
        _hitTarget = Physics2D.OverlapCircle(transform.position, _interactRange, _objectLayeer);
        
        if (_hitTarget == null)
        {
            _player.PlayerInputCompo.controlButtons.HandleAttackMode();
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
            _currentInteractObject = interactTarget;
            interactTarget.Detected();
            _player.PlayerInputCompo.controlButtons.HandleInteractMode();
        }    
    }

}