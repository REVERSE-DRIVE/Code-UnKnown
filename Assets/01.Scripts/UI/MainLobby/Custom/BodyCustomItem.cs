using PlayerPartsManage;
using UnityEngine;

public class BodyCustomItem : CustomItem
{
    [SerializeField] private PlayerBodyPartDataSO _bodyPartData;
    protected override void OnClick()
    {
        _customIcon.BodySO = _bodyPartData;
    }
}