using UnityEngine;

public class PlayerComboCounter : MonoBehaviour
{
    [Header("Combo Setting")] 
    [SerializeField] private float _comboCancelTime;
    private float _comboTime = 0;
    public bool IsCombo => _comboTime < _comboCancelTime;
    [field:SerializeField] public int comboCount { get; private set; } = 0;

    private void Update()
    {
        _comboTime += Time.deltaTime;

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
    

}