using PlayerPartsManage;
using UnityEngine;

public class LegCustomItem : CustomItem
{
    [SerializeField] private PlayerLegPartDataSO _legPartData;
    protected override void OnClick()
    {
        _customIcon.LegSO = _legPartData;
    }
}