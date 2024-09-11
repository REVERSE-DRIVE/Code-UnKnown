using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AnalyzePlayTimeScene;

public class AnalyzePlayTime : MonoBehaviour
{
    string token;
    bool needDispose = false;
    bool processSave = false;
    int autoSendTime = 60 * 5; // 5분


    AnalyzePlayTimeScene scenePlaytime;

    private void Awake() {
        AnalyzeManager.OnLogined += LoadTimeToken;

        // 종료 이벤트
        Application.wantsToQuit += WantsToQuit;
    }

    public bool WantsToQuit()
    {
        if (token == null|| processSave) { // 한번 더 누르면 급한거라서 그냥 끄게 해드림
            return true; // 아무것도 못함
        }

        processSave = true;

        SceneTimeData sceneTime = new();
        if (scenePlaytime)
            scenePlaytime.GetData(ref sceneTime);

        // 토큰이 있는경우 처리하고 끔..
        SaveAndQuit(sceneTime);

        // 유니티 에디터는 못끄게 할수 없어서 그냥 끄게 해줌
    #if UNITY_EDITOR
        return true;
    #else
        return false;
    #endif
    }

    private void Start() {
        scenePlaytime = GetComponent<AnalyzePlayTimeScene>();

        StartCoroutine(AutoSaveHandler());

        if (!AnalyzeManager.Registered) { // 등록이 안되어있음
            return;
        }

        LoadTimeToken();
    }

    private void OnDestroy() {
        AnalyzeManager.OnLogined -= LoadTimeToken;
        Application.wantsToQuit -= WantsToQuit;
    }

    async void LoadTimeToken() {
        token = await AnalyzeManager.GetTimeToken();
    }

    async void SaveAndQuit(SceneTimeData sceneTime) {
        await AnalyzeManager.SendPlayTime(token);

        if (sceneTime.scene != null && sceneTime.token != null)
            await AnalyzeManager.SendPlayTime(sceneTime.token, sceneTime.scene);

        Application.Quit();
    }

    IEnumerator AutoSaveHandler() {
        while (true) {
            yield return new WaitForSecondsRealtime(autoSendTime);
            if (token == null) continue;

            AutoSave();
        }
    }

    async void AutoSave() {
        processSave = true;
            
        string temp = token;
        token = null;

        await AnalyzeManager.SendPlayTime(temp);

        processSave = false;
        LoadTimeToken();
    }
}
