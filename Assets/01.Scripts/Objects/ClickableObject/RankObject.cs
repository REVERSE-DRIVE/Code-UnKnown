using System.Collections;
using System.Collections.Generic;
using ObjectManage;
using UnityEngine;

public class RankObject : ClickableObject
{
    [SerializeField] RankUI screen; // fuck (옛날 개발자 스러운)

    private void Awake()
    {
        OnClickEvent += OpenRank;
    }

    void OpenRank()
    {
        screen.gameObject.SetActive(true);
    }
}
