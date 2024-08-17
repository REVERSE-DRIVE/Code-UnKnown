using PlayerPartsManage;
using UnityEngine;

public class SlamShopItem : CustomItem
{
    [SerializeField] private PartType _partType;
    [SerializeField] public int _resourceAmount;
    private SlamShopPanel _slamShopPanel;

    protected override void Awake()
    {
        base.Awake();
        _slamShopPanel = FindObjectOfType<SlamShopPanel>();
    }

    private void Start()
    {
        if (_partType == PartType.Body)
        {
            var bodyPart = PartData as PlayerBodyPartDataSO;
            SetUI(bodyPart.partName, bodyPart.bodyPartSprite);
        }
        else if (_partType == PartType.Leg)
        {
            var legPart = PartData as PlayerLegPartDataSO;
            SetUI(legPart.partName, legPart.legPartSprites);
        }
    }

    public void BuyPart()
    {
        if (ResourceManager.Instance.ResourceAmount < _resourceAmount)
            return;
        ResourceManager.Instance.UseResource(_resourceAmount);
        PlayerPartManager.Instance.AddPartData(PartData);
    }

    protected override void OnClick()
    {
        _slamShopPanel.partWindow.SetChild(this);
    }
}