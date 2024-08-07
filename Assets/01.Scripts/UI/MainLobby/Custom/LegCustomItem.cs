using PlayerPartsManage;
using UnityEngine;

public class LegCustomItem : CustomItem
{
    private LegCustomPanel _legCustomPanel;

    protected override void Awake()
    {
        base.Awake();
        _legCustomPanel = FindObjectOfType<LegCustomPanel>();
    }

    protected override void OnClick()
    {
        _legCustomPanel._partWindow.SetChild(this);
    }
}