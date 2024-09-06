using UnityEngine;
using UnityEngine.UI;

public class PlayerComboCounter : MonoBehaviour
{
    [Header("Combo Setting")] 
    [SerializeField] private float _comboCancelTime;
    [SerializeField] private Image _comboGauge; 
    private float _comboTime = 0;
    public bool IsCombo => _comboTime < _comboCancelTime;
    [field:SerializeField] public int comboCount { get; private set; } = 0;

    private void Update()
    {
        _comboTime += Time.deltaTime;
        RefreshComboGauge();
    }
    
    public void CountCombo()
    {
        if (IsCombo)
            comboCount++;
        else
            comboCount = 0;

        
       
        _comboTime = 0f;
    }

    public void ResetCombo()
    {
        comboCount = 0;
    }

    public void BonusCombo(int amount)
    {
        comboCount += amount;
        _comboTime = 0f;
    }

    private void RefreshComboGauge()
    {
        if (comboCount > 30)
        {
            _comboGauge.fillAmount = 1f;
        }else if (comboCount > 20)
        {
            _comboGauge.fillAmount = 0.75f;

        }else if (comboCount > 10)
        {
            _comboGauge.fillAmount = 0.5f;

        }else if (comboCount > 5)
        {
            _comboGauge.fillAmount = 0.25f;
        }
        else
        {_comboGauge.fillAmount = 0f;
            
        }
    }

}