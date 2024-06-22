using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Button _actionButton;
    [SerializeField] private Button _skillButton;

    public Action OnMovementEvent;
    public Action OnActionEvent;
    public Action OnSkillEvent;

    private void Awake()
    {
        _actionButton.onClick.AddListener(HandleActionEvent);
        _skillButton.onClick.AddListener(HandleSkillEvent);
        
    }

    public void HandleMovementEvent()
    {
        OnMovementEvent?.Invoke();
    }

    public void HandleActionEvent()
    {
        OnActionEvent?.Invoke();
    }

    public void HandleSkillEvent()
    {
        OnSkillEvent?.Invoke();
    }


}