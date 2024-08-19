using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[InitializeOnLoad]
public static class AnalyzeEditor
{
    static AnalyzeEditor()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange status)
    {
        if (status == PlayModeStateChange.EnteredPlayMode)
            CreateCore(false);
    }

    [MenuItem("Tools/Create Analyze Core")]
    static void CreateCoreMenuItem() {
        EditorUtility.SetDirty(CreateCore(true));
    }

    static GameObject CreateCore(bool autoChange = true) {
        AnalyzeSingleton core = GameObject.FindAnyObjectByType<AnalyzeSingleton>();
        if (core != null) {
            if (!autoChange) return null;

            GameManager.DestroyImmediate(core.gameObject);
            Debug.Log("[AnalyzeCoreEditor] Changed Core!");
        }

        GameObject entity = new GameObject("[AnalyzeCore]");
        entity.AddComponent<AnalyzeSingleton>();
        entity.AddComponent<AnalyzePing>();
        entity.AddComponent<AnalyzeScene>();
        entity.AddComponent<AnalyzeErrorDetect>();
        entity.AddComponent<AnalyzePlayTime>();
        entity.AddComponent<AnalyzePlayTimeScene>();
        return entity;
    }
}
