using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyzeSingleton : MonoBehaviour
{
    static AnalyzeSingleton instance;

    private void Awake() {
        if (instance) {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    [ContextMenu("Clear Save")]
    void ClearSave() {
        PlayerPrefs.DeleteAll();
    }
}
