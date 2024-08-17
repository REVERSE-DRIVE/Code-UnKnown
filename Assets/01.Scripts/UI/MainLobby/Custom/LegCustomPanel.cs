using System.Collections.Generic;
using PlayerPartsManage;
using UnityEngine;

public class LegCustomPanel : WindowUI
{
    [SerializeField] private LegCustomItem _legCustomItemPrefab;
    [SerializeField] private Transform _content;
    
    private List<PlayerLegPartDataSO> _legCustomItems = new();
    public PartWindow _partWindow;
    private void Start()
    {
        OnFocus += SetCustomPart;
        OnClose += CloseWindow;
    }

    private void CloseWindow()
    {
        _partWindow.Close();
    }

    private void OnDestroy()
    {
        OnFocus -= SetCustomPart;
        OnClose -= CloseWindow;
    }

    private void SetCustomPart()
    {
        var partManager = PlayerPartManager.Instance;
        if (partManager.PlayerPartDataList.Count == 0)
        {
            for (int i = 0; i < _content.childCount; i++)
            {
                Destroy(_content.GetChild(i).gameObject);
            }
            _legCustomItems.Clear();
            return;
        }
        for (int i = 0; i < partManager.PlayerPartDataList.Count; i++)
        {
            if (partManager.PlayerPartDataList[i] is PlayerLegPartDataSO leg)
            {
                if (_legCustomItems.Contains(leg))
                    continue;
                var legCustomItem = Instantiate(_legCustomItemPrefab, _content);
                legCustomItem.SetUI(leg.partName, leg.legPartSprites);
                legCustomItem.PartData = leg;
                _legCustomItems.Add(leg);
            }
        }
    }
}