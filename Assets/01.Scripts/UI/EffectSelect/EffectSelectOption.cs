using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EffectSelectOption : MonoBehaviour
{
    [field :SerializeField] public PowerUpSO PowerUp { get; private set; }
    [SerializeField] private TextMeshProUGUI _titleText, _descText;
    [SerializeField] private Image _iconImage;
    private Button _selectBtn;

    private void OnValidate()
    {
        if (PowerUp != null)
        {
            UpdateUI();
        }
    }

    private void Awake()
    {
        _selectBtn = GetComponent<Button>();
        _selectBtn.onClick.AddListener(SelectPowerUp);
    }

    public void SetCardData(PowerUpSO data)
    {
        PowerUp = data;
        UpdateUI();
    }

    private void SelectPowerUp()
    {
        PowerUp.effectList.ForEach(effect => effect.UseEffect());
        UIManager.Instance.Close(WindowEnum.EffectSelect);
    }

    private void UpdateUI()
    {
        if (_titleText != null)
            _titleText.text = PowerUp.title;

        if (_descText != null)
            _descText.text = PowerUp.description;

        if (_iconImage != null)
            _iconImage.sprite = PowerUp.icon;
    }
}