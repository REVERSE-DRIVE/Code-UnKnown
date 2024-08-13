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
        {
            AnalyzeSingleton core = GameObject.FindAnyObjectByType<AnalyzeSingleton>();
            if (core != null) return;

            GameObject entity = new GameObject("[AnalyzeCore]");
            entity.AddComponent<AnalyzeSingleton>();
            entity.AddComponent<AnalyzePing>();
            entity.AddComponent<AnalyzeScene>();
            entity.AddComponent<AnalyzeErrorDetect>();
            entity.AddComponent<AnalyzePlayTime>();
            entity.AddComponent<AnalyzePlayTimeScene>();
        }
    }
}
