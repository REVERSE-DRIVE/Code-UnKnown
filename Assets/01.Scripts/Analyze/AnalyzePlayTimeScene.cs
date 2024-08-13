using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnalyzePlayTimeScene : MonoBehaviour
{
    public struct SceneTimeData {
        public string scene;
        public string token;
    }


    string lastScene;
    string token;
    bool tokenLoad = false;

    private void Awake() {
        SceneManager.activeSceneChanged += OnChangeScene;
        AnalyzeManager.OnLogined += RegisterCurrentScene;
    }

    private void Start() {
        if (AnalyzeManager.Registered) {
            RegisterCurrentScene();
        }
    }

    private void OnDestroy() {
        SceneManager.activeSceneChanged -= OnChangeScene;
        AnalyzeManager.OnLogined -= RegisterCurrentScene;
    }

    private void OnChangeScene(Scene current, Scene next)
    {
        NowScene(next.name);
    }

    void RegisterCurrentScene() => NowScene(SceneManager.GetActiveScene().name);

    void NowScene(string sceneName) {
        // 전 씬 확인
        _ = SaveTime();
        
        if (!tokenLoad) // 토큰이 로드중일떄는 그냥 그 토큰 씀
            LoadTimeToken();

        lastScene = sceneName;
    }

    async void LoadTimeToken() {
        tokenLoad = true;
        token = await AnalyzeManager.GetTimeToken();

        tokenLoad = false;
    }

    public async Task SaveTime() {
        if (lastScene == null || token == null) return;

        string temp = token;
        token = null;
        await AnalyzeManager.SendPlayTime(temp, lastScene);
    }

    public void GetData(ref SceneTimeData data) {
        data.scene = lastScene;
        data.token = token;
    }
}
