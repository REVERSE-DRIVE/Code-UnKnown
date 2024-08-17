using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnalyzeScene : MonoBehaviour
{
    private void Awake() {
        SceneManager.activeSceneChanged += OnChangeScene;
    }

    private void OnDestroy() {
        SceneManager.activeSceneChanged -= OnChangeScene;
    }

    async void OnChangeScene(Scene current, Scene next) {
        if (PlayerPrefs.GetInt($"domi.AnalyzeScene.{next.name}") == 1) return; // 이미 함

        bool success = await AnalyzeManager.SceneChange(next.name);

        if (success) {
            PlayerPrefs.SetInt($"domi.AnalyzeScene.{next.name}", 1);
        }
    }
}
