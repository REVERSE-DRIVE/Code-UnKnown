public class BodyCustomItem : CustomItem
{
    private BodyCustomPanel _bodyCustomPanel;

    protected override void Awake()
    {
        base.Awake();
        _customIcon = FindObjectOfType<CustomIcon>();
        _bodyCustomPanel = FindObjectOfType<BodyCustomPanel>();
    }

    protected override void OnClick()
    {
        _bodyCustomPanel._partWindow.SetChild(this);
    }
}