using UnityEngine;
using UnityEngine.UI;

public class ControlButtons : MonoBehaviour
{

    [Header("Button Setting")]
    [SerializeField] private Button _controlButton;

    [SerializeField] private Sprite[] _buttonSprites;
    private Image _buttonImage;

    private void Awake()
    {
        _buttonImage = _controlButton.GetComponent<Image>();

    }


    private void SetInteractMode(bool value)
    {
        _buttonImage.sprite = value ? _buttonSprites[1] : _buttonSprites[0];
    }

    public void HandleInteractMode()
    {
        SetInteractMode(true);
    }

    public void HandleAttackMode()
    {
        SetInteractMode(false);
    }

}
