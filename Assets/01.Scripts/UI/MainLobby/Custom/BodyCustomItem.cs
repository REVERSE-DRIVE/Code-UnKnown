using PlayerPartsManage;
using UnityEngine;

public class BodyCustomItem : CustomItem
{
    [SerializeField] private PlayerBodyPartDataSO _bodyPartData;
    protected override void Start()
    {
        SetUI(_bodyPartData.bodyPartSprite, _bodyPartData.partName);
    }

    protected override void OnClick()
    {
        _customIcon.ChangeIconImage(true, _bodyPartData.bodyPartSprite);
        PlayerPartManager.Instance.SetBodyPart(_bodyPartData);
    }
}