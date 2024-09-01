using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankTopUI : RankItemUI
{
    [SerializeField] TextMeshProUGUI expT;
    public override void Init(RankItemUI.Data data) {
        base.Init(data); // 기존꺼 실행
        expT.text = $"exp.{data.score:N0}";
    }

    public override void Clear()
    {
        base.Clear();
        expT.text = "";
    }
}
