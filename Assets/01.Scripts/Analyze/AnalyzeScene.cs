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

    void OnChangeScene(Scene current, Scene next) {
        print($"Scene Move {current.name} -> {next.name}");
        AnalyzeManager.SceneChange(next.name);
    }
}
