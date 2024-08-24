using PlayerPartsManage;
using UnityEngine;
using UnityEngine.UI;

public class BodySlot : PartSlot
{
    [SerializeField] private Image _image;
    private PlayerBodyPartDataSO _currentBodySO;
    
    public void Initialize(PlayerBodyPartDataSO part)
    {
        // 정보를 가지고있는 SO가 이게 아니라면 바꿔야될수도 있음
        _currentBodySO = part;
        _currentPart = part;
        RefreshImage();
    }

    public override void RefreshImage()
    {
        _image.sprite = _currentBodySO.bodyPartSprite;
    }
}