using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EffectSelectOption : MonoBehaviour
{
    [field :SerializeField] public PowerUpSO PowerUp { get; private set; }
    [SerializeField] private TextMeshProUGUI _titleText, _descText, _levelText;
    [SerializeField] private Image _iconImage;
    private Button _selectBtn;

    private void OnValidate()
    {
        // if (PowerUp != null)
        // {
        //     UpdateUI();
        // } // 얘때문에 PowerUpManager가 복사되는거였다
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
        if(PowerUp.maxCollect < 50) // 제한이 50을 초과하는 애들은 단순 즉시효과이기 때문에 제외
            PowerUpManager.Instance.ApplyPowerUp(PowerUp.id);
        
        PowerUp.effectList.ForEach(effect => effect.UseEffect());
        UIManager.Instance.Close(WindowEnum.EffectSelect);
    }

    private void UpdateUI()
    {
        if (_titleText != null)
        {
            _titleText.text = PowerUp.title;
            // 최대 개수에 따라서 레벨을 간접적으로 보여주는거 구현
        }

        if (_levelText != null)
        {
            if (PowerUp.maxCollect >= 50)
            {
                _levelText.text = "";
            }
            else
            {
                int level = PowerUpManager.Instance.Find(PowerUp.id);
                _levelText.text = level == 0 ? "NEW" : $"{level} > {level+1}{(level+1 >= PowerUp.maxCollect ? "(MAX)" : "")}";
            }
        }

        if (_descText != null)
            _descText.text = PowerUp.description;

        if (_iconImage != null)
            _iconImage.sprite = PowerUp.icon;

        _iconImage.color = PowerUp.powerUpColor;
    }
}