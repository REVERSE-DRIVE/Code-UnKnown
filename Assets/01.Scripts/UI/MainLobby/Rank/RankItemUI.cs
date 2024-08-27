using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankItemUI : MonoBehaviour
{
    public struct Data {
        public int rank;
        public string name;
        public Texture2D image;
        public long score;
    }

    [SerializeField] TextMeshProUGUI nameT;
    [SerializeField] RawImage image;
    [SerializeField] TextMeshProUGUI rankT;
    [SerializeField] TextMeshProUGUI expprefixT;

    public void Init(Data data) {
        nameT.text = data.name;
        rankT.text = data.rank.ToString();
        image.texture = data.image;
        expprefixT.text = "test";
    }
}
