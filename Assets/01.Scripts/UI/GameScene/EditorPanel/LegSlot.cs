using PlayerPartsManage;
using UnityEngine;
using UnityEngine.UI;

public class LegSlot : PartSlot
{
    [SerializeField] private Image[] _images; 
    private PlayerLegPartDataSO _currentLegSO;
    
    public void Initialize(PlayerLegPartDataSO part)
    {
        // 정보를 가지고있는 SO가 이게 아니라면 바꿔야될수도 있음
        _currentLegSO = part;
        _currentPart = part;
        RefreshImage();
    }

    public override void RefreshImage()
    {
        for (int i = 0; i < 4; i++)
        {
            _images[i].sprite = _currentLegSO.legPartSprites[i];
        }
    }
}