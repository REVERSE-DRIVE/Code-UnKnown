using DG.Tweening;
using PlayerPartsManage;
using UnityEngine;
using UnityEngine.UI;

public class CustomIcon : MonoBehaviour
{
    [SerializeField] private RectTransform _choicePanel;
    [SerializeField] private Image[] _icon;
    [SerializeField] private Ease _openEase, _closeEase;
    private Button _button;
    private bool _isOpened;
    

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ClickAnimation);
    }
    
    public void ChangeIconImage(bool isBody, params Sprite[] sprites)
    {
        if (isBody)
        {
            _icon[0].sprite = sprites[0];
            return;
        }
        for (int i = 1; i < _icon.Length; i++)
        {
            _icon[i].sprite = sprites[i-1];
        }
    }

    private void ClickAnimation()
    {
        if (DOTween.IsTweening(_choicePanel)) DOTween.Kill(_choicePanel);
        if (_isOpened)
        {
            _choicePanel.DOScaleY(0f, 0.5f).SetEase(_closeEase)
                .OnComplete(() => _choicePanel.gameObject.SetActive(false));
            _isOpened = false;
        }
        else
        {
            _isOpened = true;
            _choicePanel.gameObject.SetActive(true);
            _choicePanel.DOScaleY(1f, 0.5f).SetEase(_openEase);
        }
        
    }
}