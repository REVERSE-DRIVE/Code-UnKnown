using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyzePing : MonoBehaviour
{
    readonly string SAVE_NAME = "domi.AnalyzeRegister";

    async void Start()
    {
        if (!AnalyzeManager.Registered) { // 등록이 안되어 있으면
            bool cache = PlayerPrefs.GetInt(SAVE_NAME, 0) == 1;
            AnalyzeManager.Registered = cache;

            if (!cache) { // 등록한 적이 없음
                bool result = await AnalyzeManager.RegisterDevice();
                if (!result) return; // 등록 실패

                PlayerPrefs.SetInt(SAVE_NAME, 1);
            }
        }

        // 체크인
        AnalyzeManager.SendPing();
    }
}
