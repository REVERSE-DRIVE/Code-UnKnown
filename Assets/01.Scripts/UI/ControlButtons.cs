using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ControlButtons : MonoBehaviour
{
    [Header("Button Setting")]
    public Button actionButton;
    public Button skillButton;
    
    [SerializeField] private Sprite[] _buttonSprites;
    private Image _buttonImage;
    private bool _isInteractMode;

    public event Action OnInteractEvent;
    public event Action OnAttackEvent;
    
    public event Action OnSkillEvent;

    private void Awake()
    {
        _buttonImage = actionButton.GetComponent<Image>();
        actionButton.onClick.AddListener(HandleActionButtonClick);

    }
    private void SetInteractMode(bool value)
    {
        _buttonImage.sprite = value ? _buttonSprites[1] : _buttonSprites[0];
    }

    public void HandleInteractMode()
    {
        if(_isInteractMode) return;
        SetInteractMode(true);
        _isInteractMode = true;

    }

    public void HandleAttackMode()
    {
        if(!_isInteractMode) return;
        SetInteractMode(false);
        _isInteractMode = false;
    }

    private void HandleActionButtonClick()
    {
        if (_isInteractMode)
        {
            OnInteractEvent?.Invoke();

        }
        else
        {
            OnAttackEvent?.Invoke();
        }
    }

}
