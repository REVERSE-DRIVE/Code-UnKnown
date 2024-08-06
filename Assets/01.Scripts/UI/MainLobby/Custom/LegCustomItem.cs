using PlayerPartsManage;
using UnityEngine;

public class LegCustomItem : CustomItem
{
    [SerializeField] private PlayerLegPartDataSO _legPartData;
    protected override void Start()
    {
        SetUI(_legPartData.legPartSprites[0], _legPartData.partName);
    }

    protected override void OnClick()
    {
        _customIcon.ChangeIconImage(false, _legPartData.legPartSprites);
        PlayerPartManager.Instance.SetLegPart(_legPartData);
    }
}