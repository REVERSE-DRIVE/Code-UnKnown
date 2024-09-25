using System.Collections;
using System.Collections.Generic;
using ObjectManage;
using UnityEngine;

public class RankObject : ClickableObject
{
    [SerializeField] RankUI screen;

    private void Awake()
    {
        OnClickEvent += OpenRank;
    }

    void OpenRank()
    {
        screen.Open();
    }
}
