using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyzeSingleton : MonoBehaviour
{
    public static AnalyzeSingleton Instance { get; private set; }

    private void Awake() {
        if (Instance) {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    [ContextMenu("Clear Save")]
    void ClearSave() {
        PlayerPrefs.DeleteAll();
    }
}
