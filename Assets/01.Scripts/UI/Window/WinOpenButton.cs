using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinOpenButton : MonoBehaviour
{
    [SerializeField] WindowType type;
    Button button;

    WindowUI window;

    private void Awake() {
        button = GetComponent<Button>();
        button.onClick.AddListener(Open);
    }

    private void Start() {
        window = WindowManager.Instance.GetWindow(type);
    }

    void Open() {
        window.Open();
    }
}
