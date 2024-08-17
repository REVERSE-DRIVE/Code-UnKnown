using System.Collections.Generic;
using PlayerPartsManage;
using UnityEngine;

public class BodyCustomPanel : WindowUI
{
    [SerializeField] private BodyCustomItem _bodyCustomItemPrefab;
    [SerializeField] private Transform _content;
    
    private List<PlayerBodyPartDataSO> _bodyCustomItems = new();

    public PartWindow _partWindow;
    private void Start()
    {
        OnFocus += SetCustomPart;
    }

    private void OnDestroy()
    {
        OnFocus -= SetCustomPart;
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
            _bodyCustomItems.Clear();
            return;
        }
        for (int i = 0; i < partManager.PlayerPartDataList.Count; i++)
        {
            if (partManager.PlayerPartDataList[i] is PlayerBodyPartDataSO body)
            {
                if (_bodyCustomItems.Contains(body))
                    continue;
                var bodyCustomItem = Instantiate(_bodyCustomItemPrefab, _content);
                bodyCustomItem.SetUI(body.partName, body.bodyPartSprite);
                bodyCustomItem.PartData = body;
                _bodyCustomItems.Add(body);
            }
        }
    }
}