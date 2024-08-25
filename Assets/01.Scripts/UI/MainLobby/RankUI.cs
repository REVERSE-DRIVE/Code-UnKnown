using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankUI : MonoBehaviour
{
    readonly string RANK_ID = "CgkInoqooYweEAIQAg";

    [SerializeField] GameObject errorAlert;
    [SerializeField] Transform list;
    [SerializeField] GameObject itemBox;
    [SerializeField] Button closeBtn;

    private void Awake() {
        closeBtn.onClick.AddListener(Close);
    }

    private void OnEnable() {
        Clear();
    
        if (false) { // 만약 google 서비스에 연결 되어있지 않은 경우
            errorAlert.SetActive(true);
            return;   
        }
        
        
    }

    void Clear() {
        for (int i = 0; i < list.childCount; i++)
            Destroy(list.GetChild(0).gameObject);

        errorAlert.SetActive(false);
    }

    void Close() {
        gameObject.SetActive(false);
    }
}
