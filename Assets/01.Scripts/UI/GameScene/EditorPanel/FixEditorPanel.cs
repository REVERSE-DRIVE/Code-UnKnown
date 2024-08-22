using UnityEngine;

public class FixEditorPanel : MonoBehaviour, IWindowPanel
{
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Open()
    {
        SetCanvas(true);

    }

    public void Close()
    {
        SetCanvas(false);
    }

    private void SetCanvas(bool value)
    {
        _canvasGroup.alpha = value ? 1f : 0f;
        _canvasGroup.interactable = value;
        _canvasGroup.blocksRaycasts = value;
    }
}
